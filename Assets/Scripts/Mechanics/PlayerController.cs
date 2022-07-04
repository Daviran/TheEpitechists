using System.Collections;
using System.Collections.Generic;
using TopdownRPG.Gameplay;
using TopdownRPG.Mechanics;
using TopdownRPG.Model;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public new Rigidbody2D rigidbody;
        public PlayerInstance player;
        public bool canMove = true;
        public Animator animator;
        /*public EdouPlayer edouard;
        public DavidPlayer david;*/

        Vector2 movement;

        private void Awake()
        {
            /*switch(PlayerChoice.playerIndex)
            {
                case 0:
                    player = new EdouPlayer();
                    break;
                case 1:
                    player = new DavidPlayer();
                    break;
            }*/
        }

        private void Start()
        {
            player = GameController.player;
            player.playerController = this;
        }

        void testBoolEvent()
        {
            if (GameController.triggeredEvents["ClocheOut"] == false && Input.GetKey(KeyCode.P))
            {
                GameController.triggeredEvents["ClocheOut"] = true;
            }
        }

        void Update()
    {
            movement.x = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("Horizontal", movement.x);
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Vertical", movement.y);

            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                movement.y = 0;
                animator.SetFloat("Vertical", movement.y);
            }
            else
            {
                movement.x = 0;
                animator.SetFloat("Horizontal", movement.x);
            }
            animator.SetFloat("Speed", Mathf.Abs(movement.x + movement.y));

            testBoolEvent();
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
