using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerButton : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float horizontalSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public void LeftButton()
    {
        speed = -horizontalSpeed;
    }

    public void Stop()
    {
        speed = 0;
    }
}
