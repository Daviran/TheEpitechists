using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TopdownRPG.Mechanics;
using Assets.Scripts.UI;

public class PNJMagicDuels : MonoBehaviour
{
    public string[] dialogues = { "Tu as d�clenss� ma carte pi�ze !", "Il est l'heure pour toi de me faire face dans un duel Magic� au sommet !", "Pr�pare toi � �tre vaincu !" };
    public string[] challengeLines = { "Crois tu vraiment pouvoir me vaincre ?", "En garde !" };
    public string[] winPunchline = { "Tu ne peux rien face � mes strat�gies !", "Libre � toi de revenir essayer de me d�fier.", "Si l'envie de perdre te reprend, ahah !" };
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

    float range = 10;
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
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pnjSpeaks = false;
            canvas.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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
            /*if (transform.position.x <= (player.transform.position.x -1))
                myRigidBody.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
            if (transform.position.y <= (player.transform.position.y))
                myRigidBody.MovePosition(transform.position + Vector3.up * speed * Time.deltaTime);
            myTransform.LookAt(player.transform);*/
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
        player = FindObjectOfType<PlayerController>();
        GetNewLines(dialogues);
    }

    void Update()
    {
        if(player)
        {
            range = Vector3.Distance(transform.position, player.transform.position);
        }
        if (range < 5.50 && !triggered)
        {
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Speed", 1);
            player.canMove = false;
            triggered = true;
            AudioManager.Instance.PlayClip(false);
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
