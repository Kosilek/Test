using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public GameObject gameManagerObject;
    private Character character;

    public GameObject bullet;
    public Transform firePoint;

    private Rigidbody2D rb;

    public float speed;
    [SerializeField]private bool facingRight = true;

    public float vSpeed;
    public float distance;
    [SerializeField]private bool isGrounder;

    private Animator anim;

    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        character = gameManagerObject.GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        character.SetAnimatorJump(anim, isGrounder, rb);
        CheckingGround();
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            character.Jump(rb, vSpeed, isGrounder);
        }

        if (Input.GetMouseButtonDown(0))
        {
            character.Shoot(bullet, firePoint);
        }
    }

    private void CheckingGround()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, distance);
        Debug.Log(groundInfo.collider);
        if (groundInfo.collider != null)
        {
            isGrounder = true;
        }
        else if (groundInfo.collider == null) isGrounder = false;
    }

    private void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") != 0)
        {
            character.SetAnimaterRun(anim, 1);
            character.Run(rb, speed, direction);
            FlipPlayer(direction);
        }
        else character.SetAnimaterRun(anim, 0);
    }

    private void FlipPlayer(float direction)
    {
        if (direction > 0 && !facingRight)
        {
            facingRight = character.Flip(transform, facingRight);
           
        }
        else if (direction < 0 && facingRight)
        {
            facingRight = character.Flip(transform, facingRight);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            gameObject.GetComponent<Health>().TakeDamage(collision.gameObject.GetComponent<Health>().damage, gameObject, gameObject.GetComponent<Health>().anim);
            if (gameObject.GetComponent<Health>().health <= 0)
            {
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coins>())
        {
            collision.gameObject.GetComponent<Coins>().AddCoin();
        }                    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.down * distance);
    }

}
