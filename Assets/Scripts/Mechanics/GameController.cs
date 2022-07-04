using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Model;
using TopdownRPG.Mechanics;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public static Dictionary<string, bool> triggeredEvents = new Dictionary<string, bool>
    {
        { "piscineOver", false }, {"ClocheOut", false }, { "getKey", false }, { "freePrinter", false }, { "getPassword", false },
    };

    public static PlayerInstance player;
    public static int charaIndex;

    public static PlayerController playerDataHolder2;
    public float health;
    public string playerName;
    public float physic;
    public float techno;
    public float social;
    public float[] position;

    public Scene scene;

    // loot
    //inv stuff

    void OnEnable()
    {
        Instance = this;
    }

    private void Start()
    {

    }
    void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    void Update()
    {
        if (Instance == this) Tick();
    }

    void Tick()
    {
        Debug.Log(charaIndex);
    }
}