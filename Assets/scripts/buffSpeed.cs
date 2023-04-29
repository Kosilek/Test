using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffSpeed : MonoBehaviour
{
    [SerializeField] private float speed;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().speed += speed;
            collision.GetComponent<Player>().SpeedBonus();
            ObjectManagment.Destroy(gameObject, anim);
        }
    }
}
