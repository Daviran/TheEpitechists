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

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        boxPosition = canvas.GetComponentInChildren<RectTransform>();
        dialogueBox = boxPosition.GetComponentInChildren<RectTransform>();
        textCanvas = dialogueBox.GetComponentInChildren<Text>();
        //randomIndex = Random.Range(0, dialogs.Length);
    }
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void TriggerDialog(bool boolean, Dialogue dialogs)
    {
        Debug.Log("FRETA");
        if (boolean && Input.GetKeyDown(KeyCode.E))
        {
            canvas.enabled = true;
            Debug.Log("BITE");
            sentences.Clear();
            foreach (string sentence in dialogs.dialoguesCafet)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        if (!boolean) canvas.enabled = false;
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        textCanvas.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textCanvas.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {

    }
    void Update()
    {
        
    }
}
