using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Core;
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


    }
}

