using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public CanvasManager canvasManager;
    public List<Dictionary<string, string>> questions = new List<Dictionary<string, string>>
    {
        new Dictionary<string, string>
        {
            { "Question" , "Donnez la valeur de x: \n string[] array = new string[5] \n for( i = 0; i < array.length; i++) \n { int x = 0; x++; } " },
            { "ReponseA", "0"},
            { "ReponseB", "6" },
        },
        new Dictionary<string, string>
        {
            {"Question" , "Donnez la valeur de x: \n x = 5; y = 4; z = -2; \n x *= z - y" },
            { "ReponseA", "- 14"},
            { "ReponseB", "- 30" },
        },
        new Dictionary<string, string>
        {
            {"Question" , "Donnez la valeur de x: \n x = \"\"; y = 42; z = \"est notre Dieu � tous\"; \n x == y + z; " },
            { "ReponseA", "42est notre Dieu � tous"},
            { "ReponseB", "erreur" },
        },
    };
    public List<bool> answers = new List<bool>();
    public bool playerInRange = false;
    GameObject player;
    public static event Action OnVictory;
    public static event Action OnDefeat;


    void Start()
    {

    }

    // Question
    public void AskQuestion()
    {
        if (canvasManager.index != answers.Count) return;
        StartCoroutine(canvasManager.DisplayQuestions());
        if (canvasManager.index >= 2) canvasManager.index = 2;
    }

    // Answer

    public void GetAnswer(int answer)
    {
        switch (canvasManager.index)
        {
            case 0:
                if (answer == 0)
                {
                    answers.Add(true);
                }
                else
                {
                    answers.Add(false);
                }
                break;
            case 1:
                if (answer == 1)
                {
                    answers.Add(true);
                }
                else
                {
                    answers.Add(false);
                }
                break;
            case 2:
                if (answer == 1)
                {
                    answers.Add(true);
                }
                else
                {
                    answers.Add(false);
                }
                break;
        }
        canvasManager.index++;
        if (canvasManager.index > 2)
        {
            GetScore();
        }
        else
        {
            AskQuestion();
        }
    }

    // Submit and End
    public void GetScore()
    {
        int result = 0;
        foreach (bool answ in answers)
        {
            if (answ) result++;
        }
        if (result >= 2)
        {
            OnVictory?.Invoke();
        }
        else
        {
            OnDefeat?.Invoke();
        }
        StartCoroutine(canvasManager.DisplayResults(result));
    }

    // Try again

    public void TryAgain()
    {
        canvasManager.index = 0;
        answers.Clear();
        canvasManager.start.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        StopAllCoroutines();
        StartCoroutine(canvasManager.DisplayWelcomeText());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {

    }
}
