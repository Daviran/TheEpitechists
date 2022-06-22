using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Gameplay;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public new Rigidbody2D rigidbody;
        public PlayerInstance player;
        public bool canMove = true;
        /*public EdouPlayer edouard;
        public DavidPlayer david;*/

        Vector2 movement;

        private void Awake()
        {
            switch(PlayerChoice.playerIndex)
            {
                case 0:
                    player = new EdouPlayer();
                    break;
                case 1:
                    player = new DavidPlayer();
                    break;
            }
        }

        private void Start()
        {
            
        }
        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            if (canMove)
            {
                rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.fixedDeltaTime * movement);
            }
        }
    }

}
