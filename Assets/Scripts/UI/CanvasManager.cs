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
        start.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        computer.answers.Clear();
        index = 0;
        Time.timeScale = 1;
    }

    public IEnumerator DisplayWelcomeText()
    {
        canvasText.text = "";
        string welcome;
        /*if (computer.player.piscineSuccess)
        {
            welcome = "Vous avez d�j� r�ussi la piscine, f�licitations !";
            start.onClick.RemoveAllListeners();
            start.GetComponentInChildren<TextMeshProUGUI>().text = "Press Esc. to exit";
        }*/
        welcome = "Bienvenue � Epitech ! Pour commencer ta piscine de code, appuie sur Start";
        foreach (char letter in welcome)
        {
            canvasText.text += letter;
            yield return null;
        }
    }

    public IEnumerator DisplayQuestions()
    {
        start.gameObject.SetActive(false);
        if(reponseA.enabled)
        {
            reponseA.gameObject.SetActive(true);
            reponseB.gameObject.SetActive(true);
        }
        canvasText.text = "";
        string question = computer.questions[index]["Question"];
        foreach (char letter in question)
        {
            canvasText.text += letter;
            yield return null;
        }
        reponseA.GetComponentInChildren<TextMeshProUGUI>().text = computer.questions[index]["ReponseA"];
        reponseB.GetComponentInChildren<TextMeshProUGUI>().text = computer.questions[index]["ReponseB"];
    }

    public IEnumerator DisplayResults(int result)
    {

        reponseA.gameObject.SetActive(false);
        reponseB.gameObject.SetActive(false);
        start.gameObject.SetActive(true);

        if(result <= 2)
        {
            start.GetComponentInChildren<TextMeshProUGUI>().text = "Press \"r\" to retry";
            canvasText.text = "";
            string question = "Vous avez �chou�. Laissez-vous envahir par l'esprit de 42 et r�essayer, ou allez pointer au ch�mage, looser !";
            foreach (char letter in question)
            {
                canvasText.text += letter;
                yield return null;
            }
        } else
        {
            start.GetComponentInChildren<TextMeshProUGUI>().text = "Press Esc. to exit";
            canvasText.text = "";
            string question = "F�licitations ! Un pas de plus en direction de 42 !";
            foreach (char letter in question)
            {
                canvasText.text += letter;
                yield return null;
            }
        }
    }

    void Update()
    {
        if(computer.playerInRange && Input.GetKey(KeyCode.E))
        {
            DisplayCanvas();
        }
        //Debug.Log(canvas.enabled);
        if(canvas.isActiveAndEnabled && Input.GetKey(KeyCode.Escape))
        {
            ExitCanvas();
        }

        if(canvas.isActiveAndEnabled && Input.GetKey(KeyCode.R))
        {
            computer.TryAgain();
        }        
    }
}
