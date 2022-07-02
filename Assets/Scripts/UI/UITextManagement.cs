using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using TopdownRPG.Gameplay;
using TopdownRPG.UI;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UITextManagement : MonoBehaviour
{

    private string playerName;
    string[] introText;
    int index = 0;
    public Text text;
    public Image dialogueBox;
    public Canvas canvas;
    PlayerInstance player;
    LoadLevel loadLevel;
    Collider2D test;
    FabienneIntro fab;

    private void Awake()
    {
        fab = FindObjectOfType<FabienneIntro>();
        loadLevel = FindObjectOfType<LoadLevel>();
        player = GameController.player;
        dialogueBox.gameObject.SetActive(false);
        Debug.Log(dialogueBox.isActiveAndEnabled);
    }
    void Start()
    {
        //Time.timeScale = 0;
        Invoke(nameof(DisplayCanvas), 1f);
        playerName = "Bonjour Monsieur " + player.Name + " , vous n'avez plus d'argent, niquez-vous. Bisou";
        introText = new string[] { "...", "....", ".......", playerName };
    }

    void DisplayCanvas()
    {
        dialogueBox.gameObject.SetActive(true);
        Debug.Log(dialogueBox.isActiveAndEnabled);
    }

    void DisplayDialogue()
    {
        
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log(index);
                text.text = introText[index];
                index++;
                Debug.Log(introText.Length);
                if (index == introText.Length)
                {
                    StartCoroutine(StartCinematique());
                }
            }
        
    }

    IEnumerator StartCinematique()
    {
        yield return new WaitForSeconds(2f);
        canvas.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(false);
        Time.timeScale = 1;
        player.playerController.canMove = false;
        fab.callOver = true;
    }

    // loadLevel.LoadNextLevel(3f);

    // Update is called once per frame
    void Update()
    {
        DisplayDialogue();
    }
}
