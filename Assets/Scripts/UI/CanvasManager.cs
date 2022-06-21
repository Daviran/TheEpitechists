using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI canvasText;
    public Button reponseA;
    public Button reponseB;
    public Button start;
    public Computer computer;
    public int index = 0;

    private void Awake()
    {
        canvas.gameObject.SetActive(false);
        reponseA.gameObject.SetActive(false);
        reponseB.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
    }

    void Start()
    {
        /*reponseA.onClick.AddListener(delegate { computer.GetAnswer(0); });
        reponseB.onClick.AddListener(delegate { computer.GetAnswer(1); });*/
    }
    void DisplayCanvas()
    {
        Time.timeScale = 0;
        canvas.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(DisplayWelcomeText());
    }

    void ExitCanvas()
    {
        StopAllCoroutines();
        canvas.gameObject.SetActive(false);
        computer.player.GetComponent<PlayerController>().canMove = true;
    }

    public IEnumerator DisplayWelcomeText()
    {
        canvasText.text = "";
        string welcome = "Bienvenue à Epitech ! Pour commencer ta piscine de code, appuie sur Start";
        foreach (char letter in welcome)
        {
            canvasText.text += letter;
            yield return null;
        }
    }

    public IEnumerator DisplayQuestions()
    {
        if(reponseA.enabled)
        {
            reponseA.gameObject.SetActive(true);
            reponseB.gameObject.SetActive(true);
        }
        start.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        canvasText.text = "";
        string question = computer.questions[index]["Question"];
        foreach (char letter in question)
        {
            canvasText.text += letter;
            yield return null;
        }
        reponseA.GetComponentInChildren<TextMeshProUGUI>().text = computer.questions[index]["ReponseA"];
        reponseB.GetComponentInChildren<TextMeshProUGUI>().text = computer.questions[index]["ReponseB"];
        if(index >= 2)
        {
            start.onClick.RemoveAllListeners();
            start.onClick.AddListener(delegate { computer.GetScore(); });
        }
    }

    public IEnumerator DisplayResults(int result)
    {
        if(result < 2)
        {
            start.GetComponentInChildren<TextMeshProUGUI>().text = "Retry";
            canvasText.text = "";
            string question = "Vous avez échoué. Laissez-vous envahir par l'esprit de 42 et réessayer, ou allez pointer au chômage, looser !";
            foreach (char letter in question)
            {
                canvasText.text += letter;
                yield return null;
            }
            start.onClick.RemoveAllListeners();
            start.onClick.AddListener(delegate { computer.TryAgain(); });
        } else
        {
            start.GetComponentInChildren<TextMeshProUGUI>().text = "Exit";
            canvasText.text = "";
            string question = "Félicitations ! Un pas de plus en direction de 42 !";
            foreach (char letter in question)
            {
                canvasText.text += letter;
                yield return null;
            }
            start.onClick.RemoveAllListeners();
            start.onClick.AddListener(delegate { ExitCanvas(); });
        }
    }

    void Update()
    {
        if(computer.playerInRange && Input.GetKey(KeyCode.E))
        {
            DisplayCanvas();
        }

        if(canvas.enabled && Input.GetKey(KeyCode.Escape))
        {
            ExitCanvas();
        }
        
    }
}
