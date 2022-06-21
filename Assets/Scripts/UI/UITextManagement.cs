using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using TopdownRPG.Gameplay;
using UnityEngine.UI;
using UnityEngine;

public class UITextManagement : MonoBehaviour
{

    private string playerName;
    string[] introText;
    int index = 0;
    public Text text;
    public Image dialogueBox;
    EdouPlayer edou = new EdouPlayer();
    DavidPlayer david = new DavidPlayer();
    void Start()
    {
        dialogueBox.gameObject.SetActive(false);
        Invoke(nameof(DisplayCanvas), 1f);
        if(PlayerChoice.playerIndex == 0)
        {
            playerName = "Bonjour Monsieur " + edou.Name + " , vous n'avez plus d'argent, niquez-vous. Bisou";
        }
        else if(PlayerChoice.playerIndex == 1)
        {
            playerName = "Bonjour Monsieur " + david.Name + " , vous n'avez plus d'argent, niquez-vous. Bisou";
        }

        introText = new string[] { "...", "....", ".......", playerName };
    }

    void DisplayCanvas()
    {
        dialogueBox.gameObject.SetActive(true);
    }

    void DisplayDialogue()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(index);
            text.text = introText[index];
            index++;
            if (index > introText.Length) index = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayDialogue();
    }
}
