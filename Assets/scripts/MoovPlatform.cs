using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoovPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float positionMin;
    [SerializeField] private float positionMax;
    [SerializeField] private bool attachingPlayer;
    [SerializeField] private bool moovX;
    [SerializeField] private bool moovY;
    [SerializeField] private bool moovXY;
    private bool upwardMovement;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (moovX)
        {
            MoovX();
        }

        if (moovY)
        {
            MoovY();
        }

        if (moovXY)
        {
            MoovXY();
        }

    }

    private void MoovX()
    {
        if (upwardMovement)
        {
            rb.velocity = new Vector2(speed * (-1), rb.velocity.y);
        }
        else if (!upwardMovement)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        if (transform.position.x <= positionMin)
        {
            upwardMovement = false;
        }
        else if (transform.position.x >= positionMax)
        {
            upwardMovement = true;
        }
    }

    private void MoovY()
    {
        if (upwardMovement)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed * (-1));
        }
        else if (!upwardMovement)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }

        if (transform.position.y <= positionMin)
        {
            upwardMovement = false;
        }
        else if (transform.position.y >= positionMax)
        {
            upwardMovement = true;
        }
    }

    private void MoovXY()
    {
        if (upwardMovement)
        {
            rb.velocity = new Vector2(speed * (-1), speed * (-1));
        }
        else if (!upwardMovement)
        {
            rb.velocity = new Vector2(speed * (1), speed);
        }

        if (transform.position.x <= positionMin)
        {
            upwardMovement = false;
        }
        else if (transform.position.x >= positionMax)
        {
            upwardMovement = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attachingPlayer)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
             
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (attachingPlayer)
        {
            if (collision.gameObject.GetComponent<Player>())
            {
               
            }
        }
    }
}
/*
 *        if (upwardMovement)
        {
            rb.velocity = new Vector2(speed * (-1), rb.velocity.y);
        }
        else if (!upwardMovement)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        if (transform.position.x <= positionMin)
        {
            upwardMovement = false;
        }
        else if (transform.position.x >= positionMax)
        {
            upwardMovement = true;
        }
*/