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
    public Image spaceBar;
    PlayerInstance player;
    LoadLevel loadLevel;
    Collider2D test;
    FabienneIntro fab;
    public bool introCin = false;

    private void Awake()
    {
        fab = FindObjectOfType<FabienneIntro>();
        loadLevel = FindObjectOfType<LoadLevel>();
        player = GameController.player;
        dialogueBox.gameObject.SetActive(false);
        spaceBar.gameObject.SetActive(false);
    }
    void Start()
    {
        //Time.timeScale = 0;
        Invoke(nameof(DisplayCanvas), 1f);
        AudioManager.Instance.PhoneClip(true);
        playerName = "Bonjour Monsieur " + player.Name + " , vous n'avez plus d'argent, \nnous avons le plaisir de vous proposer des AGIOS\n s'élevant à hauteur de 95% de votre RSA. Bisou.";
        introText = new string[] { "...", "....", ".......", playerName };
    }

    void DisplayCanvas()
    {
        dialogueBox.gameObject.SetActive(true);
        spaceBar.gameObject.SetActive(true);
    }

    void DisplayDialogue()
    {

        if (Input.GetButtonDown("Jump"))
        {
            text.text = introText[index];
            if (index == 3)
            {
                AudioManager.Instance.PhoneClip(false);
            }
            index++;
        }
        if (index == introText.Length)
        {
            introCin = true;
            StartCoroutine(StartCinematique());
            spaceBar.gameObject.SetActive(false);
        }
            
        
    }

    IEnumerator StartCinematique()
    {
        yield return new WaitForSeconds(4f);
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
        if(!introCin)
        {
            DisplayDialogue();
        }
    }
}
