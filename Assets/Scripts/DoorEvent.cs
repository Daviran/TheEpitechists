using System;
using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public Animator animator;
    public Collider2D collider;
    protected bool isEnabled = true;
    bool unlock = false;

    void OnEnable()
    {
        Computer.OnVictory += UnlockDoor;
    }

    void UnlockDoor()
    {
        unlock = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.name == "Cray" && !unlock)
        {
            return;
        }
        if (true == isEnabled && collision.CompareTag("Player") || collision.CompareTag("PNJ"))
        {
            animator.SetBool("Open", true);
            StartCoroutine(Delay(false));
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (true == isEnabled && collision.CompareTag("Player") || collision.CompareTag("PNJ"))
        {
            animator.SetBool("Open", false);
            StartCoroutine(Delay(true));
        }
    }

    IEnumerator Delay(bool enabled)
    {
        yield return new WaitForSeconds(1f);
        collider.enabled = enabled;
    }

    public bool GetIsEnable()
    {
        return isEnabled;
    }

    public void SetIsEnable(bool enable)
    {
        this.isEnabled = enable;
    }
}