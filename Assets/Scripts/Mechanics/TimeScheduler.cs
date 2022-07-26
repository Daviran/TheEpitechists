using System.Collections;
using System.Collections.Generic;
using TopdownRPG.UI;
using Pathfinding;
using UnityEngine;

public class TimeScheduler : MonoBehaviour
{
    public DayByDay dayTransition;
    static int dayIndex = 0;
    private void OnEnable()
    {
        TimeManager.LunchTime += LunchTime;
        TimeManager.BackHomeTime += BackHomeTime;
        TimeManager.BackToWork += BackToWork;
    }

    void OnDisable()
    {
        TimeManager.LunchTime -= LunchTime;
        TimeManager.BackHomeTime -= BackHomeTime;
        TimeManager.BackToWork -= BackToWork;

    }
    public void LunchTime()
    {
        Debug.Log("A table !");
    }

    public void BackHomeTime()
    {
        Debug.Log("A demain les gars !");
        dayTransition.NextDayTransition(dayIndex);
        dayIndex++;
    }
    public void BackToWork()
    {
        Debug.Log("Au boulot !");
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
}
