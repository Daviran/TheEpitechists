using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    int rank = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "randomNPC")
        {
            if (rank == 5)
            {
                gameObject.transform.position = new Vector3(12.7f, 21.5f, 0);
                rank = 0;
            }
            else if (rank == 4)
            {
                gameObject.transform.position = new Vector3(-19.7f, 21.5f, 0); 
                rank++;
            }
            else if (rank == 3)
            {
                gameObject.transform.position = new Vector3(-19.7f, 66.6f, 0); 
                rank++;
            }
            else if (rank == 2)
            {
                gameObject.transform.position = new Vector3(6f, 66.6f, 0); 
                rank++;
            }
            else if (rank == 1)
            {
                gameObject.transform.position = new Vector3(6f, 59.7f, 0);
                rank++;
            }
            else if (rank == 0)
            {
                gameObject.transform.position = new Vector3(12.7f, 59.7f, 0);
                rank++;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
