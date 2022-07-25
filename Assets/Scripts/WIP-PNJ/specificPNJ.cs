using System.Collections;
using System.Collections.Generic;
using System;
using TopdownRPG.DialogWIP;
using UnityEngine;

public class specificPNJ : PNJ
{
    public static event Action OnDialogueTrigger;
    new Dialogue dialogues;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log(dialogues);
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

    void Awake()
    {
        if(gameObject.CompareTag("Fefe"))
        {
            dialogues = new Dialogue(
                new string[] { "Quel est le meilleur manga entre One Piece, Naruto et Bleach ?",
                    "J'aime bien VueJS et...",
                    "T'es all� voir la doc ?",
                    "Attends, je regarde.",
                    "T'�tais o� y a une heure ?",
                    "J'ai regard� ta question... J'ai pas trouv�.",
                    "Faudrait que tu demandes � Link",
                    "T�as fini la piscine ?" 
                });
        }
        if (gameObject.CompareTag("Cloche"))
        {
            dialogues = new Dialogue(
                new string[] { "MOI LES TRICHEURS, J'LES DYNAMITE, J'LES DISPERSE, J'LES VENTILE.",
                    "JE PEUX VOUS PROPOSER MON PIED DANS LES NOIX",
                    "VOUS SAVEZ QU'A SOLLICITER TROP SOUVENT LA PATIENCE DES GENS, ON FINIT PAR AGACER ?",
                    "FAUT TOUJOURS QUE VOUS VENIEZ FOUTRE VOTRE MERDE, VOUS...",
                    "MOI AU MOINS QUAND JE PARLEMENTE, C'EST PAS VICELARD."
                });
        }
        if (gameObject.CompareTag("Sebby"))
        {
            dialogues = new Dialogue(
                new string[] { "Vous avez pas vu ma trottinette ?",
                    "Vous saviez qu'un email a �t� flash� � 150km/h",
                    "Un adminSys a eu un probl�me...",
                    "Meilleure soir�e, j'ai d�mont� et remont� un switch jusqu'� 5h du mat' !",
                    "Quelqu'un m'a pris ma clef, il va falloir que je la refasse",
                    "Malheureusement l'imprimante 3D est utilis�e...",
                    "Ce sont probablement des adorateurs de Windaube qui ont fait le coup.",
                    "Ces fifrelins ont reni� 42.",
                    "Ils finiront dans les flammes de la doc Java.",
                    "Je vais aller chercher Cloche, il y a un individu bizarre dans les toilettes...",
                    "As-tu vu cette superbe oeuvre de Zelda accroch�e au mur dans la caf�t ?"
                });
        }
        if (gameObject.CompareTag("Morales"))
        {
            dialogues = new Dialogue(
                new string[] { "Ouuuuh Ouuuuh�", 
                    "Je suis un fant�me !",
                    "Bon d�accord, qu�est-ce que tu veux ? Le mot de passe pour t��chapper ?",
                    "Mais ce ne sera pas gratuit mon bon monsieur ! Certainement pas !",
                    "Je ne suis pas rest� coinc� ici tout ce temps pour permettre au premier venu de s��chapper � si bon compte.",
                    "Tu vois ces monstres qui pullulent sous Epitech ? Rapporte-moi 20 troph�es de monstre, et je te donnerai le mot de passe."
                });
        }
        if (gameObject.CompareTag("Cape"))
        {
            dialogues = new Dialogue(
                new string[] { "Je suis une licorne !",
                    "Zelda m�est apparue hier soir, je suis le champion de 42 !",
                    "Tu connais Magic ? C�est un jeu de cartes.",
                    "Les anciens y jouaient pour entrer en communication avec 42.",
                });
        }
        if (gameObject.CompareTag("Tonneau"))
        {
            dialogues = new Dialogue(
                new string[] { "Iel est bien votre jeu.e, iel n�a pas de genre dans vos stat.e.s !",
                    "As-tu entendu parler de notre seigneur.e 42 ?",
                });
        }
    }

    private void Update()
    {
        if(pnjSpeaks && Input.GetKeyDown(KeyCode.E))
        {
            OnDialogueTrigger?.Invoke();
        }
    }
}
