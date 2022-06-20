using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Core;
using TopdownRPG.Model;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public TopDownModel model = Simulation.GetModel<TopDownModel>();
    void OnEnable()
    {
        Instance = this;
    }
    void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    void Update()
    {
        if (Instance == this) Simulation.Tick();
    }
}
