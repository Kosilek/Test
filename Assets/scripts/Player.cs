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

    public float direction;

    [SerializeField] private GameObject button;

   // [SerializeField] private Transform spawnButton;

    private string optionMoov;
  //  [SerializeField] private float horizontalSpeed;

    private void Awake()
    {
        Physics2D.queriesStartInColliders = false;
       if (PlayerPrefs.HasKey("Option"))
       {
            optionMoov = PlayerPrefs.GetString("Option");
        }
    }
    private void Start()
    {
        switch (optionMoov)
        {
            case "keyboard":
                button.SetActive(false);
                break;
            case "button":
                button.SetActive(true);
                break;
        }
        anim = GetComponent<Animator>();
        character = gameManagerObject.GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckingGround();
        if (optionMoov == "keyboard")
        {
            character.SetAnimatorJump(anim, isGrounder, rb);          
            if (Input.GetKeyDown(KeyCode.Space))
            {
                character.Jump(rb, vSpeed, isGrounder);
            }
        }   
        if (optionMoov == "keyboard")
        {
            if (Input.GetMouseButtonDown(0))
            {
                character.Shoot(bullet, firePoint);
            }
        }
    }

    public void AttackButton()
    {
        character.Shoot(bullet, firePoint);
    }

    public void CheckingGround()
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
        character.Jump(rb, vSpeed, isGrounder);
    }

    private void FixedUpdate()
    {
        switch (optionMoov)
        {
            case "keyboard":
                MoovPlayerKeyBoard();
                break;
            case "button":
                MoovPlayerBuuton();
                break;
        }
    }

    private void MoovPlayerBuuton()
    {
        if (direction != 0)
        {
            character.SetAnimaterRun(anim, 1);
            character.Run(rb, speed, direction);
            FlipPlayer(direction);
        }
        else character.SetAnimaterRun(anim, 0);
    }
    private void MoovPlayerKeyBoard()
    {
        direction = Input.GetAxis("Horizontal");
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

        direction = -1;
    }

    public void MoovRightButton()
    {
        direction = 1;
    }

   
    public void Stop()
    {
        direction = 0;
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
