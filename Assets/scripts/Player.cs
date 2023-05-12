using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class Player : MonoBehaviour
{
    [SerializeField] private float fall;
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
    private string optionMoov;
    [HideInInspector] public bool checkFinish = false;
    [SerializeField] GameObject buttonCntr;

    [SerializeField] private float timerSpeedMax;
    [SerializeField]private float timerSpeed;
    public float startSpeed;
    public GameObject checkingWall;
    private void Awake()
    {
        LevelManager.playerSave = gameObject;
        Physics2D.queriesStartInColliders = false;
       if (PlayerPrefsSave.HasKey(MeaningString.option))
       {
            optionMoov = PlayerPrefsSave.GetString(MeaningString.option);
        }
    }
    private void Start()
    {
        startSpeed = speed;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (optionMoov == MeaningString.button)
        {
            buttonCntr.SetActive(true);
        }
        else if (optionMoov == MeaningString.keyboard)
        {
            buttonCntr.SetActive(false);
        }
    }

    private void Update()
    {
         LevelManager.playerSave.transform.position = transform.position;
        CheckingGround();
        Character.SetAnimatorJump(anim, isGrounder, rb);
        if (optionMoov == MeaningString.keyboard && checkFinish == false)
        {           
            if (Input.GetMouseButtonDown(0))
            {
                Character.Shoot(bullet, firePoint);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Character.Jump(rb, vSpeed, isGrounder);
            }
        }   
    }

    public void AttackButton()
    {
        Character.Shoot(bullet, firePoint);
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
        Character.Jump(rb, vSpeed, isGrounder);
    }

    private void FixedUpdate()
    {
        anim.SetFloat(MeaningString.speed, Mathf.Abs(direction));
            if(optionMoov == MeaningString.keyboard && checkFinish == false)
        {
            MoovPlayerKeyBoard();
        }else if (optionMoov == MeaningString.button && checkFinish == false)
        {
            Character.Run(rb, speed, direction);
        }

            if (timerSpeed > 0)
        {
            timerSpeed--;
        } else if (timerSpeed == 0)
        {
            speed = startSpeed;
        }
    }

    private void MoovPlayerKeyBoard()
    {
        direction = Input.GetAxis(MeaningString.horizontal);         
            Character.Run(rb, speed, direction);
            FlipPlayer(direction);
    }

    public void MoovLeftButton()
    {

        direction = -1;
        FlipPlayer(direction);
    }

    public void MoovRightButton()
    {
        direction = 1;
        FlipPlayer(direction);
    }
    
   
    public void Stop()
    {
        direction = 0;
    }
    private void FlipPlayer(float direction)
    {
        if (direction > 0 && !facingRight)
        {
            facingRight = Character.Flip(transform, facingRight);
           
        }
        else if (direction < 0 && facingRight)
        {
            facingRight = Character.Flip(transform, facingRight);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coins>())
        {
            collision.gameObject.GetComponent<Coins>().AddCoin();
        }        
        
        if (collision.GetComponent<Health>())
        {
            if (collision.gameObject.GetComponent<Enemy>())
            {
                Destroy(collision.gameObject.GetComponent<DamageObject>());
                collision.gameObject.GetComponent<Health>().damage = 0;
                collision.GetComponent<Health>().Death(collision.gameObject, collision.GetComponent<Enemy>().anim);
            }
        }
    }

    public void SpeedBonus()
    {
        timerSpeed = timerSpeedMax;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.down * distance);
    }
}
