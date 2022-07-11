using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static event Action LunchTime;
    public static event Action BackHomeTime;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }
    public static List<string> Week = new List<string>() { "Mardi", "Mercredi", "Jeudi", "Vendredi" };

    private float minuteToRealTime = 0.5f;
    private float timer;

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
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            if(Hour == 12 && Minute == 0)
            {
                LunchTime?.Invoke();
            }

            if(Hour == 17 && Minute == 30)
            {
                BackHomeTime?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }
}
