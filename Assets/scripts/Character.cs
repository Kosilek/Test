using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public void Run(Rigidbody2D rb ,float speed, float direction)
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }
    
    public void Jump(Rigidbody2D rb ,float vSpeed, bool isGrounder)
    {
        if (isGrounder)
        {
            rb.AddForce(new Vector2(0, vSpeed));
        }
    }

    public bool Flip(Transform transform ,bool facingRight)
    {
        transform.Rotate(0f, 180f, 0f);
        return !facingRight;
    }

    public void SetAnimaterRun(Animator anim, int speed)
    {
        anim.SetInteger("speed", speed);
    }

    public void SetAnimatorJump(Animator anim, bool isGrounder, Rigidbody2D rb)
    {
        anim.SetBool("isGrounder", isGrounder);
        anim.SetFloat("vSpeed", rb.velocity.y);
    }
}
