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
       
        if (groundInfo.collider != null)
        {
            isGrounder = true;
        }
        else if (groundInfo.collider == null) isGrounder = false;
    }

    public void ButtonJump()
    {

    }

    private void FixedUpdate()
    {
        MoovPlayerKeyBoard();
    }

    public void MoovPlayerKeyBoard()
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

    public void MoovLeftButton()
    {
       // if (facingRight)
    //        facingRight = character.Flip(transform, facingRight);
     //   character.SetAnimaterRun(anim, 1);
        character.Run(rb, speed, -1f);
    }

    public void MoovRightButton()
    {
        if (!facingRight)
            facingRight = character.Flip(transform, facingRight);
        character.SetAnimaterRun(anim, 1);
        character.Run(rb, speed, 1f);
    }

    public void AttackButton()
    {

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coins>())
        {
            Debug.Log("Coins");
            collision.gameObject.GetComponent<Coins>().AddCoin();
        }        
        
        if (collision.GetComponent<Health>())
        {
            if (collision.gameObject.GetComponent<Enemy>())
            {
                Debug.Log("Destory");
                Destroy(collision.gameObject.GetComponent<DamageObject>());
                collision.gameObject.GetComponent<Health>().damage = 0;
                collision.GetComponent<Health>().Death(collision.gameObject, collision.GetComponent<Enemy>().anim);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.down * distance);
    }

}
