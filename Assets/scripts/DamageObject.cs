using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageObject : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.name == "fireBall(Clone)")
        {
            if (other.GetComponent<Health>())
            {
                other.GetComponent<Health>().TakeDamage(damage, other.gameObject, other.gameObject.GetComponent<Health>().anim);
            }
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<Health>())
        {
            if (!collision.gameObject.GetComponent<Enemy>())
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage, collision.gameObject, collision.gameObject.GetComponent<Health>().anim);
                Debug.Log("Damage");
            }

            if (gameObject.GetComponent<DamageObject>().name == "SpikedBall(Clone)")
            {
                Debug.Log("qqqq");
                if (collision.gameObject.GetComponent<Collider2D>())
                {
                    Debug.Log("wwwwwwww");
                    Destroy(gameObject);
                }
            }
            
        }
        if (collision.gameObject.GetComponent<Enemy>())
        {
            if (gameObject.name == "Spike")
            {
                gameObject.GetComponent<Collider2D>().isTrigger  = true;
            }
        }
    }

    /*   private void OnTriggerStay2D(Collider2D collision)
       {
           if (collision.gameObject.GetComponent<Enemy>())
           {
               if (gameObject.name == "Spike")
               {
                   gameObject.GetComponent<Collider2D>().isTrigger = true;
               }
           }
       }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            if (gameObject.name == "Spike")
            {
                gameObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }
    }
}
