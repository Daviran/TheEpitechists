using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool isDisplay = false;
    public Canvas canvas;
    public Button start;
    public Button load;
    public Button exit;
    PlayerInstance player;

    private void OnEnable()
    {
        _DisplayMenu(isDisplay);
    }
    public void DisplayMenu(bool show)
    {
        if(isDisplay != show)
        {
            _DisplayMenu(show);
        }
        /*canvas.gameObject.SetActive(true);
        isDisplay = true;*/
    }

    void _DisplayMenu(bool show)
    {
        if (show)
        {
            Time.timeScale = 0;
            Debug.Log(Time.timeScale);
            canvas.gameObject.SetActive(true);
        } else
        {
            Time.timeScale = 1;
            canvas.gameObject.SetActive(false);
        }
        isDisplay = show;
    }

    public void SaveData()
    {
        SaveSystem.SaveData(player);
    }

    public void LoadData()
    {
        SaveData data = SaveSystem.LoadData();
        player.CurrentHp = data.health;
        player.Name = data.playerName;
        player.Physic = data.physic;
        player.Techno = data.techno;
        player.Social = data.social;
        player.playerController.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }

    void Start()
    {
        player = GameController.player;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            DisplayMenu(show: !isDisplay);
        }
        Debug.Log(Time.timeScale);
        /* if (Input.GetKey(KeyCode.M) && isDisplay == true)
         {
             ExitMenu();
         }*/
    }
}
