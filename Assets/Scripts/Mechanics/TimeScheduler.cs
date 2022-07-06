using System.Collections;
using System.Collections.Generic;
using TopdownRPG.UI;
using UnityEngine;

public class TimeScheduler : MonoBehaviour
{
    public DayByDay dayTransition;
    static int dayIndex = 0;
    private void OnEnable()
    {
        TimeManager.LunchTime += LunchTime;
        TimeManager.BackHomeTime += BackHomeTime;
    }

    void OnDisable()
    {
        TimeManager.LunchTime -= LunchTime;
        TimeManager.BackHomeTime -= BackHomeTime;

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
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
