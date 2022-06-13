using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJInstance : MonoBehaviour
{
    public string[] dialogues = { "Bonjour", "Au revoir", "Il fait beau non ?", "LoL > la vie", "Je ne bois pas, c'est mauvais pour la planète" };
    public Sprite[] skin = new Sprite[10];
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

    Vector3 directionVector;
    Transform myTransform;
    float speed = 7;
    Rigidbody2D myRigidBody;
    Collider2D flowers;
    Collider2D stone;
    Collider2D grass;
    Collider2D grassyStone;


    private void Awake()
    {
        flowers = GameObject.FindGameObjectWithTag("Flowers").GetComponent<Collider2D>();
        stone = GameObject.FindGameObjectWithTag("Stone").GetComponent<Collider2D>();
        grass = GameObject.FindGameObjectWithTag("Grass").GetComponent<Collider2D>();
        grassyStone = GameObject.FindGameObjectWithTag("GrassyStone").GetComponent<Collider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        chosenSkin = GetComponent<SpriteRenderer>();
        skinIndex = Random.Range(0, 9);
        chosenSkin.sprite = skin[skinIndex];
        canvas = GetComponentInChildren<Canvas>();
        boxPosition = canvas.GetComponentInChildren<RectTransform>();
        dialogueBox = boxPosition.GetComponentInChildren<RectTransform>();
        typeSound = dialogueBox.GetComponentInChildren<AudioSource>();
        typeSound.enabled = false;
        dialogueText = dialogueBox.GetComponentInChildren<Text>();
        sentences = new Queue<string>();

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

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
          //  Debug.Log("Flowers" + flowers.bounds.Contains(temp));
       if(flowers.bounds.Contains(myTransform.position)) {
            if(flowers.bounds.Contains(temp))
            {
                myRigidBody.MovePosition(temp);
            }
            else
            {
                ChangeDirection();
            }
       } else if(stone.bounds.Contains(myTransform.position)) {
           // Debug.Log("STONE" + stone.bounds.Contains(temp));
           if (stone.bounds.Contains(temp))
            {
                myRigidBody.MovePosition(temp);
            }
            else
            {
                ChangeDirection();
            }
       } else if(grass.bounds.Contains(myTransform.position))
        {
           // Debug.Log("GRASS" + grass.bounds.Contains(temp));
            if (grass.bounds.Contains(temp))
            {
                myRigidBody.MovePosition(temp);
            }
            else
            {
                ChangeDirection();
            }
       } else if(grassyStone.bounds.Contains(myTransform.position))
        {
          //  Debug.Log("GRASSYSTONE" + grassyStone.bounds.Contains(temp));
            if (grassyStone.bounds.Contains(temp))
            {
                myRigidBody.MovePosition(temp);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);

        switch(direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.down;
                break;
            case 3:
                directionVector = Vector3.left;
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
        Debug.Log(flowers.bounds.Contains(myTransform.position));
        Move();
        TriggerDialogue();
    }
}
