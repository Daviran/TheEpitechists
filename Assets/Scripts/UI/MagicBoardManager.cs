using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI
{
    public class MagicBoardManager : MonoBehaviour
    {
        /* A update -> Victoire si carte dans l'inventaire, sinon défaite */
        public bool win;

        public Canvas canvas;
        public PNJMagicDuels pnj;
        AudioSource duelMusic;
        public TextMeshProUGUI duelText;
        public bool encounterOver;
        public bool arenaUp;

        private void Awake()
        {
            canvas.gameObject.SetActive(false);
            duelMusic = GetComponent<AudioSource>();
            encounterOver = false;
            arenaUp = false;
        }

        void Start()
        {
  
        }
        void DisplayCanvas()
        {
            Time.timeScale = 0;
            canvas.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(DisplayText());
        }

        void ExitCanvas()
        {
            StopAllCoroutines();
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public IEnumerator DisplayText()
        {
            duelText.text = "";
            string taunt = "Tu n'as aucune chance de me vaincre, je suis une légende parmi les légendes !";
            foreach (char letter in taunt)
            {
                duelText.text += letter;
                yield return null;
            }
        }

        public IEnumerator WaitToLaunch()
        {
            duelMusic.Play();
            yield return new WaitForSeconds(3);
            {
                DisplayCanvas();
                yield return new WaitForSeconds(2);
                StartCoroutine(DisplayText());
            }
        }

        void Update()
        {
            if (!arenaUp && Input.GetKey(KeyCode.E) && pnj.pnjSpeaks && pnj.hasSpoken && encounterOver)
            {
                pnj.hasSpoken = false;
                arenaUp = true;
                pnj.TauntAndChallenge();
            }
            if (canvas.isActiveAndEnabled && Input.GetKey(KeyCode.Escape))
            {
                ExitCanvas();
                duelMusic.Stop();
                arenaUp = false;
                encounterOver = true;
                pnj.player.canMove = true;
                AudioManager.Instance.PlayClip(true);
            }

        }
    }
}
