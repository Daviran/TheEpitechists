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
    public GameObject capeMan;
    Rigidbody2D rb;
    PlayerController player;
    public GameObject spawnPiscine;
    string damocles = "Bonjour " + GameController.player.Name + ", sauf erreur de notre part, tu dois 5000€ à Epitech. \n" +
        "Il faudrait que tu nous donnes le chèque vendredi au plus tard. \n" +
        "Bonne journée.";
    public bool callOver = false;
    public bool racket = false;
    Animator _animator;
    Vector3 capeSpawn = new Vector3(-40, 30.1f);


    void Awake()
    {
        xPos = transform.position.x;
        yPos = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        AudioManager.Instance.PlayClip(true);
    }

    void MoveTowardsPlayer()
    {
        if (yPos < 0.1 && xPos < 1.1)
        {
            _animator.SetFloat("Speed", 0);
            StartCoroutine(DisplayMessage());
            callOver = false;
        }
        _animator.SetFloat("Speed", 1);
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", -1);
        rb.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
        if (yPos < 0.1)
        {
            _animator.SetFloat("Horizontal", -1);
            _animator.SetFloat("Vertical", 0);
            rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
        }
    }

    IEnumerator DisplayMessage()
    {
        canvas.gameObject.SetActive(true);
        backGround.gameObject.SetActive(false);
        dayOne.gameObject.SetActive(false);
        displayBox.gameObject.SetActive(true);
        text.text = "";
        _animator.SetFloat("Speed", 0);
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
        _animator.SetFloat("Horizontal", 1);
        _animator.SetFloat("Vertical", 0);
        _animator.SetFloat("Speed", 1);
        rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);

        if(xPos > 4)
        {
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 1);
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
        Instantiate(capeMan, capeSpawn, Quaternion.identity);
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
        if(player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }
}
