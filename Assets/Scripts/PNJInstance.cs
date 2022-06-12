using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJInstance : MonoBehaviour
{
    public string[] dialogues = { "Bonjour", "Au revoir", "Il fait beau non ?", "LoL > la vie", "Je ne bois pas, c'est mauvais pour la planète" };
    public Sprite[] skin = new Sprite[10];
    int skinIndex;
    float speed = 3f;
    float 
    internal PNJController pnjController;
    internal int controllerIndex = -1;
    SpriteRenderer chosenSkin;
    Canvas canvas;
    RectTransform boxPosition;
    RectTransform dialogueBox;
    Text dialogueText;
    AudioSource typeSound;
    private bool pnjSpeaks = false;
    private Queue<string> sentences;
    float x;


    private void Awake()
    {
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
        Debug.Log(sentences.Count);
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

    void PNJPath()
    {
        if (gameObject.transform.position.x != coordonnees[0].transform.position.x)
        {
            x *= Time.deltaTime;
            gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, 0);
        }
        /*gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, coordonnees[0].transform.position, 0.01f);
        if (gameObject.transform.position == coordonnees[0].transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, coordonnees[1].transform.position, 0.01f);
        }
        if (gameObject.transform.position == coordonnees[1].transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, coordonnees[2].transform.position, 0.01f);
        }
        if (gameObject.transform.position == coordonnees[2].transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, coordonnees[3].transform.position, 0.01f);
        }
        if (gameObject.transform.position == coordonnees[3].transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, coordonnees[0].transform.position, 0.01f);
        }*/
        /*if(PNJPosition.x > coordonnees[0].x)
        {
            PNJPosition.x += gameObject.transform.position.x * Time.deltaTime * speed;
        }
        if (PNJPosition.x < coordonnees[0].x)
        {
            PNJPosition.x -= gameObject.transform.position.x * Time.deltaTime * speed;
        }
        if (PNJPosition.y > coordonnees[0].y)
        {
            PNJPosition.y += gameObject.transform.position.y * Time.deltaTime * speed;
        }
        if (PNJPosition.y < coordonnees[0].y)
        {
            PNJPosition.y -= gameObject.transform.position.y * Time.deltaTime * speed;
        }*/

        /*foreach(GameObject coor in coordonnees)
        {
            coordonees.Add(coor.FindWithTag("coordonnees"));
        }
        this.gameObject.transform.position;*/
    }
    void Start()
    {
        
    }

    void Update()
    {
        PNJPath();
        TriggerDialogue();
    }
}
