using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJInstance : MonoBehaviour
{
    //public string[] sentences = { "Bonjour", "Au revoir", "Il fait beau non ?", "LoL > la vie", "Je ne bois pas, c'est mauvais pour la planète" };
    public Sprite[] skin = new Sprite[10];
    int skinIndex;
    internal PNJController pnjController;
    internal int controllerIndex = -1;
    public Dialogue dialogs;
    SpriteRenderer chosenSkin;
    Canvas canvas;
    RectTransform boxPosition;
    RectTransform dialogueBox;

    private void Awake()
    {
        chosenSkin = GetComponent<SpriteRenderer>();
        skinIndex = Random.Range(0, 9);
        chosenSkin.sprite = skin[skinIndex];
        canvas = GetComponentInChildren<Canvas>();
        boxPosition = canvas.GetComponentInChildren<RectTransform>();
        dialogueBox = boxPosition.GetComponentInChildren<RectTransform>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("plop");
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            FindObjectOfType<DialogueManager>().TriggerDialog(true, dialogs);
            //isTrigger = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            FindObjectOfType<DialogueManager>().TriggerDialog(false, dialogs);
        }
    }

    /*private void TriggerDialog()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.E))
        {
            textCanvas.text = dialogs[randomIndex];
            canvas.enabled = true;
            randomIndex++;
            if (randomIndex >= dialogs.Length) randomIndex = 0;
        } 
        
        if (!isTrigger) canvas.enabled = false;
    }*/
    void Start()
    {
        
    }

    void Update()
    {
    }
}
