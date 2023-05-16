using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Animations;
[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class Player : MonoBehaviour
{
    private float startSpeed;
    private bool facingRight = true;
    private bool isGrounder;
    private string optionMoov; 
    public float speed;
    public float vSpeed;
    public float distance;
    private Rigidbody2D rb;
    private Animator anim;
    [HideInInspector] public bool checkFinish = false;
    [HideInInspector] public float direction;
    [SerializeField] private float timerSpeedMax;
    [SerializeField]private float timerSpeed;
  //  [SerializeField] private RuntimeAnimatorController animVoodoo;
  //  [SerializeField] private RuntimeAnimatorController animDragon;
 //   [SerializeField] private RuntimeAnimatorController animKnight;
    [SerializeField] GameObject buttonCntr;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;

    private void Awake()
    {
        LevelManager.playerSave = gameObject;
        Physics2D.queriesStartInColliders = false;
       if (PlayerPrefsSave.HasKey(MeaningString.option))
       {
            optionMoov = PlayerPrefsSave.GetString(MeaningString.option);
       }
     /*  switch(Character.playersSelect)
        {
                case 0:
            anim.GetComponent<Animator>().runtimeAnimatorController = animVoodoo;
            break; 
                case 1:
            anim.GetComponent<Animator>().runtimeAnimatorController = animDragon;
            break;*/
         //       case 2:
         //   anim.GetComponent<Animator>().runtimeAnimatorController = animKnight;
          //  break;

      //  }
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
