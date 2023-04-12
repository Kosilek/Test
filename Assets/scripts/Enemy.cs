using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   // public GameObject gameManagerObject;
    //private Character character;
    [SerializeField] private float distance;
    [SerializeField] private float rayDistance;
    [SerializeField] private float horizontalDistance;
    [SerializeField] private float blockDistance;
    [SerializeField] private float playerDistance;
    [SerializeField] private Transform groundDetectionTransform;
    [SerializeField] private Transform blockDetection;
    private RaycastHit2D groundInfo;
    private RaycastHit2D cliffInfo;
    private RaycastHit2D horizontalInfo;
    private RaycastHit2D blockInfo;
    private RaycastHit2D playerDetection;
    private Rigidbody2D rb;
    public Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float vSpeed;
    private bool isGrounder;
    private bool facingRight = true;
    private float direction = 1;
    string difficulty;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficulty = PlayerPrefs.GetString("Difficulty");
        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       // character = gameManagerObject.GetComponent<Character>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        Character.SetAnimatorJump(anim, isGrounder, rb);

        groundInfo = Physics2D.Raycast(transform.position, Vector2.down * distance);

        cliffInfo = Physics2D.Raycast(groundDetectionTransform.position, Vector2.down, rayDistance);

        if (groundInfo.collider != null) { isGrounder = true; }
        else if (groundInfo.collider == null) isGrounder = false;
        switch (difficulty)
        {
            case "hard":
                JumpEnemy();
                break;
        }
    }

    private void JumpEnemy()
    {
        if (facingRight == true)
        {
            horizontalInfo = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, horizontalDistance);
            Jump(horizontalInfo);
        }
        else if (facingRight == false)
        {
            horizontalInfo = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, horizontalDistance);
            Jump(horizontalInfo);
        }
    }

    private void Jump(RaycastHit2D horizontalInfo)
    {
        if (horizontalInfo)
        {
            CheckBlock(horizontalInfo, "Ground");
        }
    }

    private void CheckBlock(RaycastHit2D horizontalInfo, string block)
    {
        if (horizontalInfo.collider.gameObject.name == block)
        {
            Character.Jump(rb, vSpeed, isGrounder);
        }
    }

    private void FixedUpdate()
    {
        switch (difficulty)
        {
            case "easy":
                FlipEnemy();
                Character.Run(rb, speed, direction);
                break;
            case "normal":
                difficultyEnemy();
                break;
            case "hard":
                difficultyEnemy();
                break;
        }
        
    }

    private void difficultyEnemy()// для нормала и харда
    {
        FlipEnemyTrigger();
        FlipEnemy();
        Character.Run(rb, speed, direction);
    }

    private void FlipEnemyTrigger()
    {  
        if (facingRight)
        {
            playerDetection = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, playerDistance);
        } else if (!facingRight)
            {
                playerDetection = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, playerDistance);
            }
        if (playerDetection)
        {
            if (playerDetection.collider.GetComponent<Player>() && !facingRight)
            {
                direction = 1;
                facingRight = Character.Flip(transform, facingRight);
            }
            else if (playerDetection.collider.GetComponent<Player>() && facingRight)
            {
                direction = -1;
                facingRight = Character.Flip(transform, facingRight);
            }
        }
    }

    private void FlipEnemy()
    {
        if (facingRight)
        {
            blockInfo = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.right, blockDistance);
        } else if (!facingRight)
        {
            blockInfo = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.left, blockDistance);
        }
        if (blockInfo)
        {
            if (blockInfo.collider.name == "Ground" &&  !facingRight)
            {
                direction = 1;
                facingRight = Character.Flip(transform, facingRight);
            } else if (blockInfo.collider.name == "Ground" && facingRight)
            {
                direction = -1;
                facingRight = Character.Flip(transform, facingRight);
            }
        }
        if (cliffInfo == false  && !facingRight)
        {
            direction = 1;
            facingRight = Character.Flip(transform, facingRight);

        }
        else if (cliffInfo == false && facingRight)
        {
            direction = -1;
            facingRight = Character.Flip(transform, facingRight);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distance);

        Gizmos.DrawLine(groundDetectionTransform.position, groundDetectionTransform.position + Vector3.down * rayDistance);

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * horizontalDistance);

        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * horizontalDistance);

        Gizmos.DrawLine(blockDetection.position, blockDetection.position + Vector3.right * blockDistance);

        Gizmos.DrawLine(blockDetection.position, blockDetection.position + Vector3.left * blockDistance);
    }
}
