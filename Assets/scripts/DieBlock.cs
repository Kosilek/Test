using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            Destroy(collision.gameObject);
        }
    }
  
}
