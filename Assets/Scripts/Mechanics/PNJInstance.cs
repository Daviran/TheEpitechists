using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PNJInstance : MonoBehaviour
{
    public string[] dialogues = { "Bonjour", "Au revoir", "Il fait beau non ?", "LoL > la vie", "Je ne bois pas, c'est mauvais pour la planète" };
    public Sprite[] skin = new Sprite[1];
    int skinIndex;
    internal int controllerIndex = -1;
    SpriteRenderer chosenSkin;
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

    /*public Collider2D[] coords;
    int coordsIndex;
    public Collider2D[] rooms;
    public Collider2D presentRoom;
    bool wandering = true;
    bool travelingDelay = false;*/

    float moveTimeSeconds;
    float moveTime = 5;


    private void Awake()
    {
        moveTimeSeconds = moveTime;
        myRigidBody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        chosenSkin = GetComponent<SpriteRenderer>();
        // skinIndex = UnityEngine.Random.Range(0, 0);
        chosenSkin.sprite = skin[0];
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
        if(pnjSpeaks && Input.GetKeyDown(KeyCode.E))
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

    /*private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        if(wandering)
        {
            foreach(Collider2D room in rooms)
            {
                if(room.bounds.Contains(myTransform.position)) {
                    presentRoom = room;
                    Debug.Log(presentRoom);
                }
            }

            Debug.Log(presentRoom.bounds.Contains(temp));

            if (presentRoom.bounds.Contains(temp))
            {
                myRigidBody.MovePosition(temp);
            } else
            {
                ChangeDirection();
            }
        } else
        {
            Debug.Log("WANDERING " + wandering);
            float step = speed * Time.deltaTime;
            int trueIndex = coordsIndex + 1;
            if (trueIndex > (coords.Length -1)) trueIndex = 0;
            myTransform.position = Vector3.MoveTowards(myTransform.position, coords[trueIndex].transform.position, step);
            if (coords[trueIndex].bounds.Contains(myTransform.position) && hasard == 0)
            {
                Debug.Log("HASARD " + hasard);
                hasard++;
                if (hasard > 3) hasard = 0;
                travelingDelay = true;
                wandering = true;
                StopAllCoroutines();
                StartCoroutine(SetTravelingDelay());
            } else if(coords[trueIndex].bounds.Contains(myTransform.position) && hasard != 0)
            {
                myTransform.position = Vector3.MoveTowards(myTransform.position, coords[trueIndex + 1].transform.position, step);
                hasard++;
                if (hasard > 3) hasard = 0;
                wandering = true;
                travelingDelay = true;
                StopAllCoroutines();
                StartCoroutine(SetTravelingDelay());
            }
        }
    }*/

    void Wander()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;

        if(wanderArea.bounds.Contains(temp))
        {
            myRigidBody.MovePosition(temp);
        } else
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
        switch(direction)
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
        while(temp == directionVector && loops < 100)
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
        if(moveTimeSeconds <= 0)
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
