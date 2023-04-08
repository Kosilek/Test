using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject gameManagerObject;
    private Character character;

    [SerializeField] private float distance;
    [SerializeField] private float rayDistance;
    [SerializeField] private float horizontalDistance;
    [SerializeField] private float vertical;
    [SerializeField]private Transform groundDetectionTransform;
    private RaycastHit2D groundInfo;
    private RaycastHit2D cliffInfo;
    private RaycastHit2D horizontalInfo;

    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float vSpeed;
    private bool isGrounder;
    private bool facingRight = true;
    private float direction =1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = gameManagerObject.GetComponent<Character>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        groundInfo = Physics2D.Raycast(transform.position, Vector2.down * distance);

        cliffInfo = Physics2D.Raycast(groundDetectionTransform.position, Vector2.down, rayDistance);

       // horizontalInfo = Physics2D.Raycast(transform.position, Vector2.right * horizontalDistance);

       // horizontalInfo = Physics2D.Raycast(transform.position, Vector2.left * horizontalDistance);

        if (groundInfo.collider != null) {  isGrounder = true;}
        else if (groundInfo.collider == null) isGrounder = false;

      //  if (horizontalInfo == false)
      //  {
         //   if (horizontalInfo.collider.gameObject.name != "Player")
           // {
           //     character.Jump(rb, vSpeed, isGrounder);
         //   }
      //  }
    }

    private void FixedUpdate()
    {
        FlipEnemy();
        character.Run(rb, speed, direction);
    }

    private void FlipEnemy()
    {
        //cliffInfo == false && 
        if (cliffInfo == false && !facingRight)
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
    }
}
