using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerStatistics : MonoBehaviour
{
    private Player player;
    private float startSpeed;
    private float timeSpeed;

    private void Start()
    {
        player = GetComponent<Player>();
        startSpeed = player.speed;
    }

    private void Update()
    {
        if (startSpeed != player.speed)
        {
            ControlSpeed();
            Invoke("Wait", 10f);
        }
    }

    private void ControlSpeed()
    {
        timeSpeed = 0f;
        while (timeSpeed <= 10f) 
        {
            timeSpeed += Time.deltaTime;
        }
        player.speed = startSpeed;
    }

    private void Wait()
    {

    }
}
