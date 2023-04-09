using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject[] button;
    private void Start()
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
    }
    private void Update()
    {
        if (GameObject.Find("Player(Clone)") == null)
        {
            button[0].SetActive(true); button[1].SetActive(true);
        }
    }
}
