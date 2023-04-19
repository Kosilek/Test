using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEmptyWall : MonoBehaviour
{

    [SerializeField] private GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (wall.activeInHierarchy)
            {
                wall.SetActive(false);
            }
            else if (!wall.activeInHierarchy)
            {
                wall.SetActive(true);
            }
        }
    }
}
