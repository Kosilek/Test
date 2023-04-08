using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Awake()
    {
        Instantiate(player, transform.position, transform.rotation);
    }
}
