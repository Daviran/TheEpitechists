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
        bool isSit = false;
        public Animator animator;
        /*public EdouPlayer edouard;
        public DavidPlayer david;*/

        Vector2 movement;

        void Awake()
        {
         
        }

        void Start()
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

        public void SetSit(bool sit)
        {
            isSit = sit;
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.CompareTag("PNJ"))
            {
                isSit = false;
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
            if (canMove && !isSit)
            {
                rigidbody.MovePosition(rigidbody.position + moveSpeed * Time.fixedDeltaTime * movement);
            }
        }
    }

}
