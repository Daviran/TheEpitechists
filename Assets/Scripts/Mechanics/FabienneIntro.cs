using System;
using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using TopdownRPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FabienneIntro : MonoBehaviour
{
    float xPos;
    float yPos;
    public float speed = 7;
    public Image backGround;
    public Image displayBox;
    public Text text;
    public Canvas canvas;
    public TextMeshProUGUI dayOne;
    Rigidbody2D rb;
    PlayerController player;
    public GameObject spawnPiscine;
    string damocles = "Bonjour " + GameController.player.Name + ", sauf erreur de notre part, tu dois 5000€ à Epitech. \n" +
        "Il faudrait que tu nous donnes le chèque vendredi au plus tard. \n" +
        "Bonne journée.";
    LoadLevel loadLevel;
    public bool callOver = false;
    public bool racket = false;


    void Awake()
    {
        loadLevel = FindObjectOfType<LoadLevel>();
        xPos = transform.position.x;
        yPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void MoveTowardsPlayer()
    {
        if (yPos < 0.1 && xPos < 1.1)
        {
            StartCoroutine(DisplayMessage());
            callOver = false;
        }
        rb.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
        if (yPos < 0.1) rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
    }

    IEnumerator DisplayMessage()
    {
        canvas.gameObject.SetActive(true);
        backGround.gameObject.SetActive(false);
        dayOne.gameObject.SetActive(false);
        displayBox.gameObject.SetActive(true);
        text.text = "";
        foreach (char letter in damocles)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.04f);
        }

        racket = true;
        StartCoroutine(EndCinematique());
    }

    IEnumerator EndCinematique()
    {
        yield return new WaitForSeconds(2f);
        displayBox.gameObject.SetActive(false);
        rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);

        if(xPos > 4)
        {
            rb.MovePosition(transform.position + Vector3.up * speed * Time.deltaTime);
        }
        if (yPos > 7)
        {
            StartCoroutine(PrepareBeginningGame());
        }
        /*loadLevel.LoadNextLevel(2);*/
    }

    IEnumerator PrepareBeginningGame()
    {
        displayBox.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
        backGround.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        player.gameObject.transform.position = spawnPiscine.transform.position;
        displayBox.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
        backGround.gameObject.SetActive(false);
        player.canMove = true;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        if(callOver)
        {
            MoveTowardsPlayer();
        }
        if(racket)
        {
            StartCoroutine(EndCinematique());
        }
    }
}
