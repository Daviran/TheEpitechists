using System;
using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using TMPro;
using UnityEngine;

public class DayByDay : MonoBehaviour
{

    public TextMeshProUGUI thisDay;
    public Canvas dayTransition;
    PlayerController player;

    private void Awake()
    {
        dayTransition.gameObject.SetActive(false);
        player = FindObjectOfType<PlayerController>();
    }
    public void NextDayTransition(int index)
    {
        dayTransition.gameObject.SetActive(true);
        thisDay.text = $"{TimeManager.Week[index]} - 9h30";
        BeginANewDay();
    }

    private void BeginANewDay()
    {
        player.transform.position = Vector2.zero;
    }
}
