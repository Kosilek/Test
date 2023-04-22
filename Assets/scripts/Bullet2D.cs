using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

   
}
