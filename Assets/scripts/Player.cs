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
    //  [SerializeField] private Sprite image;
    //  [SerializeField] private Sprite imageAttack;
    //   [SerializeField] private Transform[] spawnBlock;
    [SerializeField] GameObject buttonCntr;

    [SerializeField] private float timerSpeedMax;
    [SerializeField]private float timerSpeed;
    private float startSpeed;
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
        
      /*  if (optionMoov == "button")
        {
          CreateButtonControl("Left", spawnBlock[0], 90, image);
            CreateEventButton(GameObject.Find("Left"));
          CreateButtonControl("Right", spawnBlock[1], -90, image);
            CreateEventButton(GameObject.Find("Right"));
          CreateButtonControl("Jump", spawnBlock[2], 0, image);
            CreateEventButton(GameObject.Find("Jump"));
          CreateButtonControl("Attack", spawnBlock[3], 0, imageAttack);
            CreateEventButton(GameObject.Find("Attack"));
        }*/
    }

    

  /*  public Vector3 GetPlayerTransform()
    {
        return transform.position;
    }*/


  /*  private void CreateEventButton(GameObject button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = new EventTrigger.TriggerEvent();
        EventTrigger.Entry entryStop = new EventTrigger.Entry();
        entryStop.eventID = EventTriggerType.PointerUp;
        entryStop.callback = new EventTrigger.TriggerEvent();
        if (button == GameObject.Find("Left"))
        {
            entry.callback.AddListener(MoovLeftButton);
        }
        if (button == GameObject.Find("Right"))
        {
            entry.callback.AddListener(MoovRightButton);
        }
        if (button == GameObject.Find("Jump"))
        {
            entry.callback.AddListener(ButtonJump);
        }
        if (button == GameObject.Find("Attack"))
        {
            entry.callback.AddListener(AttackButton);
        }
        entryStop.callback.AddListener(Stop);
        trigger.triggers.Add(entry);
        trigger.triggers.Add(entryStop);
    }*/

    private void Update()
    {
         LevelManager.playerSave.transform.position = transform.position;
       // LevelManager.playerSave = gameObject;
        CheckingGround();
        Character.SetAnimatorJump(anim, isGrounder, rb);
        if (optionMoov == MeaningString.keyboard)
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
      //  rb = GetComponent<Rigidbody2D>();
        Character.Jump(rb, vSpeed, isGrounder);
    }

    private void FixedUpdate()
    {
        anim.SetFloat(MeaningString.speed, Mathf.Abs(direction));
            if(optionMoov == MeaningString.keyboard)
        {
            MoovPlayerKeyBoard();
        }else if (optionMoov == MeaningString.button)
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

    /*private void CreateButtonControl(string nameButton, Transform spawnBlock, float rotationZ, Sprite sprite)
    {
        GameObject newButton = new GameObject(nameButton, typeof(Image), typeof(Button), typeof(LayoutElement), typeof(EventTrigger));
        newButton.transform.SetParent(spawnBlock);
        newButton.GetComponent<RectTransform>().position = new Vector3(spawnBlock.position.x, spawnBlock.position.y, spawnBlock.position.z);
        RectTransform rtButton = newButton.GetComponent<RectTransform>();
        rtButton.anchorMin = new Vector2(0, 0);
        rtButton.anchorMax = new Vector2(1, 1);
        rtButton.anchoredPosition = new Vector2(0, 0);
        rtButton.sizeDelta = new Vector2(0, 0);
        newButton.GetComponent<Image>().sprite = sprite;
        newButton.transform.Rotate(0f, 0f, rotationZ);
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.down * distance);
    }
}
