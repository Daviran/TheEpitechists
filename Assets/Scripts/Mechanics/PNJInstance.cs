using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PNJInstance : MonoBehaviour
{
    public string[] dialogues = {
            "Quand 42 veut du bien à un homme, il vient habiter son code.",
            "42 ne saurait faire de back sans front.",
            "42 préfère Linux.",
            "Il n’est que 42 qu’on puisse aimer sans bug.",
            "Si 42 ne fait l’API, il n’est de relation back - front qui vaille.",
            "La compile nous vient de 42, les bugs de nous - mêmes.",
            "Nul ne va à 42 sans passer par Epitech.",
            "Le code est fait pour l’Homme, et l’Homme est fait pour 42.",
            "Louez 42 car il est grand !",
            "42 sera sans pitié pour les tricheurs.",
            "Prends garde de ne pas tricher, ou 42 te jettera dans les flammes de la doc Java.",
            "42 est lent à la colère, mais celle - ci est implacable.",
            "Que 42 terrasse ses ennemis !",
            "Que tous les langages soient unis sous 42 !",
            "un jour, 42 a compté jusque l’infini … deux fois.",
            "42 est puissante dans ta famille Luke"
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
    Transform myTransform;
    float speed = 7;
    Rigidbody2D myRigidBody;

    public Collider2D wanderArea;

    float moveTimeSeconds;
    float moveTime = 5;


    private void Awake()
    {
        moveTimeSeconds = moveTime;
        myRigidBody = GetComponent<Rigidbody2D>();
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

    void Wander()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;

        if (wanderArea.bounds.Contains(temp))
        {
            myRigidBody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    void Travel()
    {

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

    private void OnCollisionEnter2D(Collision2D collision)
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


    void PNJPath()
    {

    }
    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        moveTimeSeconds -= Time.deltaTime;
        if (moveTimeSeconds <= 0)
        {
            moveTimeSeconds = moveTime;
            ChangeDirection();
        }
        if (!pnjSpeaks)
        {
            Wander();
            animator.SetFloat("Speed", 1);
        }
        else
        {
            TriggerDialogue();
            animator.SetFloat("Speed", 0);
        }
    }
}
