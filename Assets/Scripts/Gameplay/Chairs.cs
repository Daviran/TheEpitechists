using System;
using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine;

public class Chairs : MonoBehaviour
{
    PlayerController player;
    bool isSit = false;
    PNJInstance pnj;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            isSit = true;
        }
        if(collision.CompareTag("PNJ"))
        {
            pnj = collision.gameObject.GetComponent<PNJInstance>();
            pnj.transform.position = transform.position;
            pnj.transform.rotation = transform.rotation;
            pnj.SetSit(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSit = false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(isSit && Input.GetButtonDown("Interract"))
        {
            player.transform.position = transform.position;
            player.transform.rotation = transform.rotation;
            player.SetSit(true);
            player.animator.SetBool("Sit", true);
        }
        if(isSit && Input.GetButtonDown("Cancel") && player != null)
        {
            player.canMove = true;
            player.SetSit(false);
            player.animator.SetBool("Sit", false);
        }
    }
}
