using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class Policien : MonoBehaviour
{
    public static event Action OpenDoor;

    public List<string> dialogues = new List<string>() { "Quel est le meilleur manga entre One Piece, Naruto et Bleach ?",
                    "J'aime bien VueJS et...",
                    "T'es all� voir la doc ?",
                    "Attends, je regarde.",
                    "T'�tais o� y a une heure ?",
                    "J'ai regard� ta question... J'ai pas trouv�.",
                    "Faudrait que tu demandes � Link",
                    "T�as fini la piscine ?"
                };

    internal int controllerIndex = -1;
    internal PNJController pnjController;
    Canvas canvas;
    RectTransform boxPosition;
    RectTransform dialogueBox;
    Text dialogueText;
    AudioSource typeSound;
    private bool pnjSpeaks = false;
    bool victoryMove = false;
    private Queue<string> sentences;
    Animator animator;
    Rigidbody2D rb;

    Vector3 directionVector;
    Vector2 movePosition;
    Vector3 policienPosition;
    float xPos;
    float yPos;
    float speed = 7;
    private void OnEnable()
    {
        Computer.OnVictory += Celebrating;
        Computer.OnDefeat += CryingDefeat;
    }

    private void OnDisable()
    {
        Computer.OnVictory -= Celebrating;
        Computer.OnDefeat -= CryingDefeat;
    }

    private void CryingDefeat()
    {
        dialogues.Clear();
        dialogues.Add("Tu commences bien l�ann�e... Allez, va finir ta journ�e�");
        dialogues.Add("T'as lu la doc ?");
        dialogues.Add("Euh non, je sais pas, t'as demand� � Link ?");
    }

    private void Celebrating()
    {
        dialogues.Clear();
        dialogues.Add("D�j� ? Jamais compris cette techno moi�");
        dialogues.Add("F�licitations en tout cas !");
        dialogues.Add("Tu as m�rit� une pause.");
        dialogues.Add("Il faudra que tu reviennes aider tes camarades qui en ont besoin !");
        victoryMove = true;
        OpenDoor?.Invoke();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        policienPosition = transform.position;
        yPos = policienPosition.y - 2;
        xPos = policienPosition.x - 5;
        canvas = GetComponentInChildren<Canvas>();
        boxPosition = canvas.GetComponentInChildren<RectTransform>();
        dialogueBox = boxPosition.GetComponentInChildren<RectTransform>();
        typeSound = dialogueBox.GetComponentInChildren<AudioSource>();
        typeSound.enabled = false;
        dialogueText = dialogueBox.GetComponentInChildren<Text>();
        sentences = new Queue<string>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pnjSpeaks = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pnjSpeaks = false;
            canvas.enabled = false;
            if (sentences != null)
            {
                sentences.Clear();
            }
        }
    }

    private void TriggerDialogue()
    {
        if (Input.GetButtonDown("Interract"))
        {
            typeSound.enabled = true;
            typeSound.Play();
            typeSound.loop = false;
            if (sentences.Count == 0)
            {
                foreach (string sentence in dialogues)
                {
                    sentences.Enqueue(sentence);
                    Debug.Log(sentences);
                }
            }
            DisplayNextSentence();
        }
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            canvas.enabled = false;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //typeSound.enabled = false;
    }

    IEnumerator TypeSentence(string sentence)
    {
        canvas.enabled = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }

    void Move()
    {
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
        animator.SetFloat("Speed", 1);
        rb.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= 47)
        {
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", 1);
            rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
        }
        if (transform.position.x <= -38)
        {
            animator.SetFloat("Speed", 0);
            victoryMove = false;
        }
    }

    void ChangeDirection()
    {
        int direction = UnityEngine.Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                directionVector = Vector3.right;
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
                break;
            case 1:
                directionVector = Vector3.up;
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);
                break;
            case 2:
                directionVector = Vector3.down;
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
                break;
            case 3:
                directionVector = Vector3.left;
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        if (pnjSpeaks)
        {
            TriggerDialogue();
        }

        if (victoryMove)
        {
            Move();
        }
    }
}