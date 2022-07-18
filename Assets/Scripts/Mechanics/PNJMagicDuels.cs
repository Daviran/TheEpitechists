using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TopdownRPG.Mechanics;
using Assets.Scripts.UI;

public class PNJMagicDuels : MonoBehaviour
{
    public string[] dialogues = { "Tu as déclenssé ma carte pièze !", "Il est l'heure pour toi de me faire face dans un duel Magic™ au sommet !", "Prépare toi à être vaincu !" };
    public string[] challengeLines = { "Crois tu vraiment pouvoir me vaincre ?", "En garde !" };
    public string[] winPunchline = { "Tu ne peux rien face à mes stratégies !", "Libre à toi de revenir essayer de me défier.", "Si l'envie de perdre te reprend, ahah !" };
    internal PNJMagicDuels PnjMagicDuels;
    Canvas canvas;
    RectTransform boxPosition;
    RectTransform dialogueBox;
    Text dialogueText;
    AudioSource typeSound;
    Animator animator;
    Vector3 directionVector;
    Transform myTransform;
    Rigidbody2D myRigidBody;
    public MagicBoardManager magicBoard;
    public AudioSource duelistsEyesMeet;
    public PlayerController player;
    private Queue<string> sentences;

    float moveTimeSeconds;
    float moveTime = 5;
    float speed = 5;

    float range;
    public bool hasSpoken = false;
    public bool triggered = false;
    public bool pnjSpeaks = false;

    private void Awake()
    {
        moveTimeSeconds = moveTime;
        myRigidBody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        canvas = GetComponentInChildren<Canvas>();
        boxPosition = canvas.GetComponentInChildren<RectTransform>();
        dialogueBox = boxPosition.GetComponentInChildren<RectTransform>();
        typeSound = dialogueBox.GetComponentInChildren<AudioSource>();
        duelistsEyesMeet = GetComponent<AudioSource>();
        typeSound.enabled = false;
        dialogueText = dialogueBox.GetComponentInChildren<Text>();
        sentences = new Queue<string>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pnjSpeaks = true;
    }

    public void TauntAndChallenge()
    {
        if (sentences.Count == 0 && !hasSpoken)
        {
            GetNewLines(challengeLines);
        }
        else
        {
            DisplayNextSentence();
            if (Input.GetKeyDown(KeyCode.E))
            {
                DisplayNextSentence();
            }
        }
    }

    private void TriggerDialogue(string[] dialogue)
    {
        if(pnjSpeaks && !hasSpoken && sentences.Count == dialogue.Length)
        {
            StartCoroutine(TypeSentence(dialogue[0]));
            sentences.Dequeue();
            
        }
        if(pnjSpeaks && !hasSpoken && Input.GetKeyDown(KeyCode.E))
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
            hasSpoken = true;
            duelistsEyesMeet.Stop();
            magicBoard.StartCoroutine(magicBoard.WaitToLaunch());
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

    IEnumerator GoToPlayer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!pnjSpeaks)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    public void GetNewLines(string[] lines)
    {
        foreach (string sentence in lines)
        {
            sentences.Enqueue(sentence);
        }
    }

    void Start()
    {
         GetNewLines(dialogues);
    }

    void Update()
    {

        range = Vector3.Distance(transform.position, player.transform.position);
        if (range < 5.50 && !triggered)
        {
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Speed", 1);
            player.canMove = false;
            triggered = true;
            duelistsEyesMeet.Play();
        }
        if(triggered && !hasSpoken && !pnjSpeaks)
        {

            StartCoroutine(GoToPlayer(3));
        }
        if (pnjSpeaks && !hasSpoken)
        {
            player.canMove = false;
            TriggerDialogue(dialogues);
        }
    }
}
