using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TopdownRPG.Mechanics;

public class PNJMagicDuels : MonoBehaviour
{
    public string[] dialogues = { "Tu as déclenssé ma carte pièze !", "Il est l'heure pour toi de me faire face dans un duel Magic™ au sommet !", "Prépare toi à être vaincu !" };
    int skinIndex;
    internal int controllerIndex = -1;
    internal PNJMagicDuels PnjMagicDuels;
    Canvas canvas;
    RectTransform boxPosition;
    RectTransform dialogueBox;
    Text dialogueText;
    AudioSource typeSound;
    private bool pnjSpeaks = false;
    private Queue<string> sentences;
    Animator animator;
    public PlayerController player;
    
    Vector3 directionVector;
    Transform myTransform;
    float speed = 7;
    Rigidbody2D myRigidBody;

    public Collider2D MagicArena;
    
    float moveTimeSeconds;
    float moveTime = 5;

    float range;
    bool hasSpoken = false;

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
        player = FindObjectOfType<PlayerController>();
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
            DisplayNextSentence();
        }
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            canvas.enabled = false;
            player.canMove = true;
            hasSpoken = true;
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

    void GoToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
        pnjSpeaks = true;
    }


    void PNJPath()
    {

    }
    void Start()
    {
         foreach (string sentence in dialogues)
            {
                sentences.Enqueue(sentence);
            }
    }

    void Update()
    {
        if(range < 10.00 && !pnjSpeaks && !hasSpoken)
        {
            player.canMove = false;
            GoToPlayer();
            animator.SetFloat("Speed", 1);

        } else
        {
            TriggerDialogue();
            animator.SetFloat("Speed", 0);

        }
    }
}
