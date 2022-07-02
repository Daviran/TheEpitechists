using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopdownRPG.Mechanics
{
    public class Cloche : MonoBehaviour
    {

        public string[] sentences = new string[] { "pouet" };
        public bool peaceful;
        public PlayerController target;
        float speed = 3f;

        private void Awake()
        {
            
        }
        public void Chasing()
        {
            if(!peaceful)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else
            {
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            target = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            peaceful = GameController.triggeredEvents["ClocheOut"];
            Chasing();
        }
    }

}
