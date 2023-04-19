using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSpikes : MonoBehaviour
{
    [SerializeField] private GameObject ballSpikes;
    [SerializeField] private Transform spawnBlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Instantiate(ballSpikes, spawnBlock.position, spawnBlock.rotation);
            gameObject.SetActive(false);
            Invoke("SpawnActive", 4f);
        }
    }

    private void SpawnActive()
    {
        gameObject.SetActive (true);
    }
}
