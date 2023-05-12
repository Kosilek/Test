using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishPlayer : MonoBehaviour
{
    public float timerStart = 0;
    public float timerFinishPlayer;

    private void Update()
    {
        timerStart += Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            timerFinishPlayer = timerStart;
            Event.SendFinishTimer(timerFinishPlayer);
            Event.SendFinish();
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            collision.GetComponent<Player>().direction = 0f;
            Destroy(collision.GetComponent<Player>());

        }
    }
}
