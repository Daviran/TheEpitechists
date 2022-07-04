using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent: MonoBehaviour
{
    public Animator animator;
    public Collider2D collider;
    protected bool isEnabled = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (true == isEnabled & "player2" == collision.name)
        {            
            animator.SetBool("Open", true);
            StartCoroutine(Delay(false));
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (true == isEnabled & "player2" == collision.name)
        { 
            animator.SetBool("Open", false);
            StartCoroutine(Delay(true));
        }
    }

    IEnumerator Delay(bool enabled)
    { 
        yield return new WaitForSeconds(1f);
        collider.enabled = enabled;
    }

    public bool GetIsEnable()
    {
        return isEnabled;
    }

    public void SetIsEnable(bool enable)
    { 
        this.isEnabled = enable;
    }
}
