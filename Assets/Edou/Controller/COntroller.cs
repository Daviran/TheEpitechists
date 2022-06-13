using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COntroller : MonoBehaviour
{

    /*    [SerializeField] float speed = 2f;
        Rigidbody2D rb;
        Animator anim;
        bool lookRight = true;*/

    private Rigidbody2D myRB;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Awake()
    {
/*        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();*/

        myRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /*        float move = Input.GetAxis("Horizontal");
                transform.Translate(Vector2.right * move * speed * Time.deltaTime);
                anim.SetFloat("Speed", Mathf.Abs(move));

                if(move > 0 && !lookRight)
                {
                    Flip();
                }
                else if(move < 0 && lookRight)
                {
                    Flip();
                }*/

        myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
    }

/*    void Flip()
    {
        lookRight = !lookRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }*/
}
