using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public Queue<string> sentences = new Queue<string>();

    void OnEnable()
    {
        Instance = this;
        Debug.Log(Instance);
    }

    void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    public void TriggerDialogue(Dialogue dialogue, PNJ pnj)
    {
        // AudioManager.dialogue.PlaySound();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Debug.Log(pnj);
        CanvasManager.Instance.FindSpeakingPNJ(pnj);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            CanvasManager.Instance.ExitCanvas();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(CanvasManager.Instance.TypeSentence(sentence));
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
