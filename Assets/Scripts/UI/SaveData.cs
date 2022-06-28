using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float health;
    public string playerName;
    public float physic;
    public float techno;
    public float social;
    public float[] position;
    Scene scene;

    public SaveData(PlayerInstance player)
    {
        health = player.CurrentHp;
        playerName = player.Name;
        physic = player.Physic;
        techno = player.Techno;
        social = player.Social;
        Vector3 playerPos = player.playerController.transform.position;
        position = new float[] { playerPos.x, playerPos.y, playerPos.z };
        scene = GameController.Instance.scene;
    }
}
