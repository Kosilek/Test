using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject gameManagerObject;
    private Character character;
  //  private Character.MoovCharacter moovCharater;

    [SerializeField] private float distance;
    [SerializeField] private float rayDistance;
    [SerializeField] private float horizontalDistance;
    [SerializeField] private float blockDistance;
    [SerializeField]private Transform groundDetectionTransform;
    [SerializeField] private Transform blockDetection;
    private RaycastHit2D groundInfo;
    private RaycastHit2D cliffInfo;
    private RaycastHit2D horizontalInfo;
    private RaycastHit2D blockInfo;

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float vSpeed;
    private bool isGrounder;
    private bool facingRight = true;
    private float direction =1;
    private void Start()
    {
     //   moovCharater = gameManagerObject.GetComponent<Character.MoovCharacter>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        character = gameManagerObject.GetComponent<Character>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        character.SetAnimatorJump(anim, isGrounder, rb);

        groundInfo = Physics2D.Raycast(transform.position, Vector2.down * distance);

        cliffInfo = Physics2D.Raycast(groundDetectionTransform.position, Vector2.down, rayDistance);

        if (groundInfo.collider != null) {  isGrounder = true;}
        else if (groundInfo.collider == null) isGrounder = false;

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
            character.Jump(rb, vSpeed, isGrounder);
            }
    }

    private void FixedUpdate()
    {
        FlipEnemy();
        character.Run(rb, speed, direction);
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
                facingRight = character.Flip(transform, facingRight);
            } else if (blockInfo.collider.name == "Ground" && facingRight)
            {
                direction = -1;
                facingRight = character.Flip(transform, facingRight);
            }
        }
        if (cliffInfo == false  && !facingRight)
        {
            direction = 1;
            facingRight = character.Flip(transform, facingRight);

        }
        else if (cliffInfo == false && facingRight)
        {
            direction = -1;
            facingRight = character.Flip(transform, facingRight);
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
