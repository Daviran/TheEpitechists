using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Aer : MonoBehaviour
{
    public static event Action OpenDoor;
    public string[] dialogues = { "...",
          ".....",
          ".........",
          "Quel est le mot de passe ? ... 42 ...?",
          "Félicitations !!!!",
    };
    internal int controllerIndex = -1;
    internal PNJController pnjController;
    Canvas canvas;
    RectTransform boxPosition;
    RectTransform dialogueBox;
    Text dialogueText;
    AudioSource typeSound;
    private bool pnjSpeaks = false;
    private Queue<string> sentences;
    Animator animator;
    Vector3 directionVector;
    Vector3 policienPosition;
    Transform myTransform;
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
        dialogues = new string[] { "..."};
    }
    private void Celebrating()
    {
        dialogues = new string[] { "Quel est le mot de passe ? ... 42 ...?", "Félicitations !!!!" };
        OpenDoor?.Invoke();
    }
    private void Awake()
    {
        myTransform = GetComponent<Transform>();
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
        if (collision.tag == "Player")
        {
            pnjSpeaks = true;
            TriggerDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pnjSpeaks = false;
            canvas.enabled = false;
        }
    }
    private void TriggerDialogue()
    {
        if (pnjSpeaks && Input.GetKeyDown(KeyCode.E))
        {
            typeSound.enabled = true;
            typeSound.Play();
            typeSound.loop = false;
            foreach (string sentence in dialogues)
            {
                sentences.Enqueue(sentence);
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
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
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
    }
}