using System;
using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine;

public class Chairs : MonoBehaviour
{
    PlayerController player;
    bool isSit = false;
    bool isInRange = false;
    PNJInstance pnj;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
            player = collision.gameObject.GetComponent<PlayerController>();
            isSit = true;
            Debug.Log(player);
        }
        if(collision.CompareTag("PNJ"))
        {
            pnj = collision.gameObject.GetComponent<PNJInstance>();
            pnj.transform.SetPositionAndRotation(transform.position, transform.rotation);
            pnj.SetSit(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(isSit && Input.GetButtonDown("Interract") && isInRange)
        {
            isSit = false;
            player.transform.SetPositionAndRotation(transform.position, transform.rotation);
            player.SetSit(true);
            player.animator.SetBool("Sit", true);
        }
        if (!isSit && Input.GetButtonDown("Cancel") && isInRange)
        {
            player.SetSit(false);
            player.animator.SetBool("Sit", false);
        }
    }
}
