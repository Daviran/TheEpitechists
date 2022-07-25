using System.Collections;
using System.Collections.Generic;
using TopdownRPG.DialogWIP;
using UnityEngine;

public class classicPNJ : PNJ
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pnjSpeaks = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pnjSpeaks = false;
            CanvasManager.Instance.ExitCanvas();
        }
    }

    private void Update()
    {
        if(pnjSpeaks && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.TriggerDialogue(dialogues, this);
        }
    }
}
