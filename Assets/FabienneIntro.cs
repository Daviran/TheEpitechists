using System;
using System.Collections;
using System.Collections.Generic;
using TopdownRPG.UI;
using UnityEngine;
using UnityEngine.UI;

public class FabienneIntro : MonoBehaviour
{
    float xPos;
    float yPos;
    public float speed = 5;
    public Image backGround;
    public Image displayBox;
    public Text text;
    public Canvas canvas;
    Rigidbody2D rb;
    string damocles = "Bonjour " + GameController.player.Name + ", sauf erreur de notre part, tu dois 5000€ à Epitech. \n" +
        "Il faudrait que tu nous donnes le chèque vendredi au plus tard. \n" +
        "Bonne journée.";
    LoadLevel loadLevel;
    public bool callOver = false;


    void Awake()
    {
        loadLevel = FindObjectOfType<LoadLevel>();
        xPos = transform.position.x;
        yPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

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
        displayBox.gameObject.SetActive(true);
        text.text = "";
        foreach (char letter in damocles)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(EndCinematique());
    }

    IEnumerator EndCinematique()
    {
        yield return new WaitForSeconds(2f);
        loadLevel.LoadNextLevel(2);
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
    }
}
