using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    Canvas canvas;
    RectTransform boxPosition;
    RectTransform dialogueBox;
    Text textCanvas;
    int randomIndex;
    bool isTrigger = false;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        boxPosition = canvas.GetComponentInChildren<RectTransform>();
        dialogueBox = boxPosition.GetComponentInChildren<RectTransform>();
        textCanvas = dialogueBox.GetComponentInChildren<Text>();
        randomIndex = Random.Range(0, dialogs.Length);
    }
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void TriggerDialog()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.E))
        {
            textCanvas.text = dialogs[randomIndex];
            canvas.enabled = true;
            randomIndex++;
            if (randomIndex >= dialogs.Length) randomIndex = 0;
        }

        if (!isTrigger) canvas.enabled = false;
    }
    void Update()
    {
        
    }
}
