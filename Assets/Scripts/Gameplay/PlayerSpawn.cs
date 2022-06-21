using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Core;
using TopdownRPG.Model;
using UnityEngine;

namespace TopdownRPG.Gameplay
{
    public class PlayerSpawn : MonoBehaviour
    {
        public GameObject[] playerList;
        public GameObject spawnPoint;
        public void SpawnCharacter()
        {
            int index = PlayerChoice.playerIndex;
            Instantiate(playerList[index], spawnPoint.transform.position, Quaternion.identity);
        }

        private void Start()
        {
            SpawnCharacter();
        }

    }

}
