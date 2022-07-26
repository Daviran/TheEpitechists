using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Model;
using TopdownRPG.Mechanics;
using TopdownRPG.UI;
using UnityEngine;

namespace TopdownRPG.Gameplay
{
    public class PlayerChoice : MonoBehaviour
    {
        public LoadLevel loadScene;

        /*private void Start()
        {
            loadScene = GetComponent<LoadLevel>();
        }*/
        public void CharacterChoose(int index)
        {
            GameController.charaIndex = index;
            switch (index)
            {
                case 0:
                    GameController.player = new EdouPlayer();
                    break;
                case 1:
                    GameController.player = new DavidPlayer();
                    break;
                case 2:
                    GameController.player = new CarmeloPlayer();
                    break;
                default:
                    break;
            }
            loadScene.LoadNextLevel(1f);

        }

        public void TestCharacter()
        {

        }

    }
}
