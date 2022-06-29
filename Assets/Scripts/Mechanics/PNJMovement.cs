using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJMovement : MonoBehaviour
{
    Vector3 directionVector;
    float speed = 3f;
    Rigidbody2D myRigidBody;
    public List<Collider2D> wanderAreas;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Wander()
    {
        Vector3 temp = transform.position + directionVector * speed * Time.deltaTime;
        foreach (Collider2D wanderAera in wanderAreas)
        {
            if (wanderAera.bounds.Contains(temp))
            {
                myRigidBody.MovePosition(temp);
            }
            else
            {
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        int direction = UnityEngine.Random.Range(0, 4);

        switch (direction)
        {
            case 0:
                directionVector = Vector3.right;
                break;
            case 1:
                directionVector = Vector3.up;
                break;
            case 2:
                directionVector = Vector3.down;
                break;
            case 3:
                directionVector = Vector3.left;
                break;
            default:
                break;
        }
    }

    void Travel()
    {

    }

    void DealCollision()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
