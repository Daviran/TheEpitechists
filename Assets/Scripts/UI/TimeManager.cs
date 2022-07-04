using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action LunchTime;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 1f;
    private float timer;

    int getTime = 0;
    void Start()
    {
        Minute = 30;
        Hour = 9;
        timer = minuteToRealTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if(timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if(Minute >= 60)
            {
                Hour++;
                getTime++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            if(getTime == 3)
            {
                LunchTime?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }
}
