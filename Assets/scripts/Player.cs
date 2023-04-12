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
    [SerializeField] private Sprite image;
    [SerializeField] private Sprite imageAttack;
    [SerializeField] private Transform[] spawnBlock;

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
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (optionMoov == "button")
        {
          CreateButtonControl("Left", spawnBlock[0], 90, image);
            CreateEventButton(GameObject.Find("Left"));
          CreateButtonControl("Right", spawnBlock[1], -90, image);
            CreateEventButton(GameObject.Find("Right"));
          CreateButtonControl("Jump", spawnBlock[2], 0, image);
            CreateEventButton(GameObject.Find("Jump"));
          CreateButtonControl("Attack", spawnBlock[3], 0, imageAttack);
            CreateEventButton(GameObject.Find("Attack"));
        }
    }

    private void CreateEventButton(GameObject button)
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
    }

    private void Update()
    {
        CheckingGround();
        Character.SetAnimatorJump(anim, isGrounder, rb);
        if (optionMoov == "keyboard")
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

    public void AttackButton(BaseEventData pointData)
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

    public void ButtonJump(BaseEventData pointData)
    {
        Character.Jump(rb, vSpeed, isGrounder);
    }

    private void FixedUpdate()
    {
        anim.SetFloat("speed", Mathf.Abs(direction));
            if(optionMoov == "keyboard")
        {
            MoovPlayerKeyBoard();
        }else if (optionMoov == "button")
        {
            Character.Run(rb, speed, direction);
        }


    }

    private void MoovPlayerKeyBoard()
    {
        direction = Input.GetAxis("Horizontal");         
            Character.Run(rb, speed, direction);
            FlipPlayer(direction);
    }

    public void MoovLeftButton(BaseEventData pointData)
    {

        direction = -1;
        FlipPlayer(direction);
    }

    public void MoovRightButton(BaseEventData pointData)
    {
        direction = 1;
        FlipPlayer(direction);
    }

   
    public void Stop(BaseEventData pointData)
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

    private void CreateButtonControl(string nameButton, Transform spawnBlock, float rotationZ, Sprite sprite)
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
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.down * distance);
    }
}
