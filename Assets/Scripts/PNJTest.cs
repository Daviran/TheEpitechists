using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJTest : PNJInstance
{
    //new public string[] dialogues = { "Yo", "VACHIAY", "On joue � LoL ?", "Le jeu vid�o paie bien", "Je ne bois pas, lol si." };

    void Start()
    {
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
        TriggerDialogue();
    }
}
