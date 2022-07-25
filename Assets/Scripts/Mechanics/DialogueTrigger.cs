using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] TextAsset inkJSON;
    bool playerInRange;

    void Awake()
    {
        playerInRange = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (playerInRange)
        {
            if(Input.GetButtonDown("Interract"))
            {
                Debug.Log(inkJSON.text);
            }
        }
    }
}
