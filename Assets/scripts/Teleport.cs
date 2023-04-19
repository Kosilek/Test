using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform finishTeleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.transform.position = new Vector3(finishTeleport.position.x, finishTeleport.position.y + 0.5f, finishTeleport.position.z);
            if (finishTeleport.GetComponent<Teleport>())
            {
                finishTeleport.gameObject.SetActive(false);
                Invoke("ActiveTeleport", 4f);
            }
        }
    }

    private void ActiveTeleport()
    {
        finishTeleport.gameObject.SetActive(true);
    }
}


