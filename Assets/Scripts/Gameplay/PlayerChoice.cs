using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Core;
using TopdownRPG.Model;
using TopdownRPG.UI;
using UnityEngine;

namespace TopdownRPG.Gameplay
{
    public class PlayerChoice : MonoBehaviour
    {
        public static int playerIndex;
        public LoadLevel loadScene;

        /*private void Start()
        {
            loadScene = GetComponent<LoadLevel>();
        }*/
        public void CharacterChoose(int index)
        {
            playerIndex = index;
            loadScene.LoadNextLevel();

        }

        public void TestCharacter()
        {
            Debug.Log(playerIndex);
        }

    }
}
