using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public Queue<string> sentences;

    void OnEnable()
    {
        Instance = this;
    }

    void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    public void TriggerDialogue(string[] dialogue, PNJInstance pnj)
    {
        // AudioManager.dialogue.PlaySound();
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }
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
