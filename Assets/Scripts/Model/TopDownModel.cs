using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Mechanics;
using UnityEngine;

namespace TopdownRPG.Model
{
    [System.Serializable]
    public class TopDownModel
    {
        public Cinemachine.CinemachineVirtualCamera virtualCamera;

        public PlayerController player;

        public Transform spawnPoint;

        public PlayerInstance playerChoice = new EdouPlayer();

        public PlayerInstance CharacterSelection(string choice)
        {
            switch(choice)
            {
                case "edouard":
                    playerChoice = new EdouPlayer();
                    break;
                case "david":
                    playerChoice = new DavidPlayer();
                    break;
                default:
                    break;
            }
            return playerChoice;
        }
    }
}

