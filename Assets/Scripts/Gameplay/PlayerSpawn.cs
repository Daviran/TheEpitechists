using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Model;
using UnityEngine;

namespace TopdownRPG.Gameplay
{
    public class PlayerSpawn : MonoBehaviour
    {
        public GameObject[] playerList;
        public GameObject spawnPoint;
        int charaIndex;
        public void SpawnCharacter()
        {
            Instantiate(playerList[charaIndex], spawnPoint.transform.position, Quaternion.identity);
        }

        private void Awake()
        {
            charaIndex = GameController.charaIndex;
            Debug.Log("ICI " + charaIndex);
        }

        private void Start()
        {
            SpawnCharacter();
        }

    }

}