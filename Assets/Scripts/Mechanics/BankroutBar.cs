using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BankroutBar : MonoBehaviour
{
    Slider _slider;
    float _countDown;
    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void Start()
    {

    }

    void OnEnable()
    {
        TimeManager.OnMinuteChanged += BankroutComing;
    }

    void OnDisable()
    {
        TimeManager.OnMinuteChanged -= BankroutComing;
    }

    void BankroutComing()
    {
        _countDown = (float)TimeManager.Minute / 60 / 40;
        _slider.value -= _countDown;
    }

    void Update()
    {
        
    }
}
