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
    public bool pnjSpeaks = false;
    private Queue<string> sentences;
    public Collider2D coordonnees;

    Vector3 directionVector;
    Vector3 coor;
    Transform myTransform;
    public float speed = 2;
    Rigidbody2D myRigidBody;
    Zoning zones;
    public bool canMove = true;
    public bool obstacles = false;
    RaycastHit2D[] results;
    int intTest = 0;
    bool traveling = false;
    int shuffle;


    private void Awake()
    {
        zones = GetComponent<Zoning>();
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
            canMove = false;
            pnjSpeaks = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canMove = true;
            pnjSpeaks = false;
            canvas.enabled = false;
        }
    }

    public void TriggerDialogue()
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

    public void StartDialogue()
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

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            canvas.enabled = false;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        typeSound.enabled = false;
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

    public void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        float step = speed * Time.deltaTime;
        Collider2D zone = zones.zones[0];
        

        foreach (Collider2D z in zones.zones)
        {
            if (z.bounds.Contains(myTransform.position))
            {
                zone = z;
            }
        }
        if (!traveling)
        {
            shuffle = Random.Range(0, 18);
        }
        if (traveling && obstacles)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, myTransform.position, step);
            int nextDirUp = myRigidBody.Cast(Vector2.up, results, Mathf.Infinity);
            Debug.Log(nextDirUp);
            int nextDirDown = myRigidBody.Cast(Vector2.down, results, Mathf.Infinity);
            int nextDirRight = myRigidBody.Cast(Vector2.right, results, Mathf.Infinity);
            int nextDirLeft = myRigidBody.Cast(Vector2.left, results, Mathf.Infinity);
        }
        if (intTest >= 1000) traveling = true;
        if(traveling)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, zones.zones[shuffle].transform.position, step);
            intTest = 0;
        }
        if (intTest >= 1000 && !obstacles)
        {
            int shuffle = Random.Range(0, 18);
            myTransform.position = Vector3.MoveTowards(myTransform.position, zones.zones[shuffle].transform.position, step);
            Debug.Log("DESTINATION " + zones.zones[shuffle].transform.position);
            intTest = 0;
            traveling = true;
                
        } else if(intTest >= 1000 && obstacles)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, myTransform.position, step);
            int nextDirUp = myRigidBody.Cast(Vector2.up, results, Mathf.Infinity);
            Debug.Log(nextDirUp);
            int nextDirDown = myRigidBody.Cast(Vector2.down, results, Mathf.Infinity);
            int nextDirRight = myRigidBody.Cast(Vector2.right, results, Mathf.Infinity);
            int nextDirLeft = myRigidBody.Cast(Vector2.left, results, Mathf.Infinity);

        }
        if(zone.bounds.Contains(myTransform.position) && !obstacles && !traveling)
        {
            coor = zone.transform.position;
            intTest++;
            myRigidBody.MovePosition(temp);
        } else if(!traveling)
        {
            ChangeDirection();
        }

        

            /*myTransform.position = Vector3.MoveTowards(myTransform.position, coor, step);

            ChangeDirection();*/
    }

    public void ChangeDirection()
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        obstacles = true;
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
        if(canMove)
        {
            Move();
        }
        TriggerDialogue();
    }
}
