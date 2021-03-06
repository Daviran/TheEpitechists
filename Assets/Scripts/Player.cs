using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRB;
    [SerializeField]
    private float speed;
    public Animator animator;
    public Vector3 movement;

    Dictionary<string, object> parameters = new Dictionary<string, object>()
{
    { "fabulousString", "hello there" },
    { "sparklingInt", 1337 },
    { "spectacularFloat", 0.451f },
    { "peculiarBool", true },
};

    void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        movement = Vector3.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove()
    {
        MoveCharacter();

        if (movement != Vector3.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetBool("IsMobile", true);
        } else
        {
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetBool("IsMobile", false);
        }
    }

    void MoveCharacter()
    {
        if (
                (0 != Input.GetAxisRaw("Horizontal") && 0 == Input.GetAxisRaw("Vertical"))
                ||
                (0 != Input.GetAxisRaw("Vertical") && 0 == Input.GetAxisRaw("Horizontal"))
                )
        {
            Debug.Log(Time.deltaTime);
            myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
        }

        if ((0 == Input.GetAxisRaw("Horizontal") && 0 == Input.GetAxisRaw("Vertical")))
        {
            Debug.Log(Time.deltaTime);
            myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
        }
    }
}