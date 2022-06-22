using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public CanvasManager canvasManager;
    /*public Canvas canvas;
    public TextMeshProUGUI canvasText;
    public Button button1;
    public Button button2;*/
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
            {"Question" , "Donnez la valeur de x: \n x = \"\"; y = 42; z = \"est notre Dieu à tous\"; \n x == y + z; " },
            { "ReponseA", "42est notre Dieu à tous"},
            { "ReponseB", "erreur" },
        },
    };
    private List<bool> answers = new List<bool>();
    public bool playerInRange = false;
    public GameObject player;
    

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
        Debug.Log(canvasManager.index);
        switch(canvasManager.index)
        {
            case 0:
                if(answer == 0)
                {
                    answers.Add(true);
                    canvasManager.reponseA.interactable = false;
                    Debug.Log(canvasManager.reponseA.IsInteractable());
                    if (canvasManager.reponseB.interactable == false)
                    {
                        answers.RemoveAt(answers.ToArray().Length - 1);
                        canvasManager.reponseB.interactable = true;
                        Debug.Log(canvasManager.reponseB.IsInteractable());
                    }
                    canvasManager.index++;
                }
                else
                {
                    answers.Add(false);
                    canvasManager.reponseB.interactable = false;
                    Debug.Log(canvasManager.reponseA.IsInteractable());
                    if (canvasManager.reponseA.interactable == false)
                    {
                        answers.RemoveAt(answers.ToArray().Length - 1);
                        canvasManager.reponseA.interactable = true;
                        Debug.Log(canvasManager.reponseB.IsInteractable());
                    }
                    canvasManager.index++;
                }
                break;
            case 1:
                if (answer == 1)
                {
                    answers.Add(true);
                    canvasManager.reponseA.interactable = false;
                    if (canvasManager.reponseB.interactable == false)
                    {
                        answers.RemoveAt(answers.ToArray().Length - 1);
                        canvasManager.reponseB.interactable = true;
                    }
                }
                else
                {
                    answers.Add(false);
                    canvasManager.reponseB.interactable = false;
                    if (canvasManager.reponseA.interactable == false)
                    {
                        answers.RemoveAt(answers.ToArray().Length - 1);
                        canvasManager.reponseA.interactable = true;
                    }
                }
                break;
            case 2:
                if (answer == 0)
                {
                    answers.Add(true);
                    canvasManager.reponseA.interactable = false;
                    if (canvasManager.reponseB.interactable == false)
                    {
                        answers.RemoveAt(answers.ToArray().Length - 1);
                        canvasManager.reponseB.interactable = true;
                    }
                }
                else
                {
                    answers.Add(false);
                    canvasManager.reponseB.interactable = false;
                    if (canvasManager.reponseA.interactable == false)
                    {
                        answers.RemoveAt(answers.ToArray().Length - 1);
                        canvasManager.reponseA.interactable = true;
                    }
                }
                break;
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
        StartCoroutine(canvasManager.DisplayResults(result));
    }

    // Try again

    public void TryAgain()
    {
        canvasManager.index = 0;
        answers.Clear();
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

