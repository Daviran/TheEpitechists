using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class PNJ : MonoBehaviour
{
    public Dialogue dialogues =
        new Dialogue(
            new string[] {
            "Quand 42 veut du bien à un homme, il vient habiter son code.",
            "42 ne saurait faire de back sans front.",
            "42 préfère Linux.",
            "Il n’est que 42 qu’on puisse aimer sans bug.",
            "Si 42 ne fait l’API, il n’est de relation back - front qui vaille.",
            "La compile nous vient de 42, les bugs de nous - mêmes.",
            "Nul ne va à 42 sans passer par Epitech.",
            "Le code est fait pour l’Homme, et l’Homme est fait pour 42.",
            "Louez 42 car il est grand !",
            "42 sera sans pitié pour les tricheurs.",
            "Prends garde de ne pas tricher, ou 42 te jettera dans les flammes de la doc Java.",
            "42 est lent à la colère, mais celle - ci est implacable.",
            "Que 42 terrasse ses ennemis !",
            "Que tous les langages soient unis sous 42 !",
            "un jour, 42 a compté jusque l’infini … deux fois.",
            "42 est puissante dans ta famille Luke"
            }
            );
    public bool pnjSpeaks = false;
    public int controllerIndex = -1;
    public PNJController pnjController;

    public virtual void Wander()
    {

    }
}
