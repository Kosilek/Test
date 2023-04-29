using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Healthh : MonoBehaviour
{
    [SerializeField] private float hill;
    Animator anim;
    private Health buffHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         buffHealth = collision.GetComponent<Health>();
        if (collision.GetComponent<Player>())
        {
            if (buffHealth.maxHealth >= buffHealth.health + hill)
            {
                buffHealth.health += hill;
            }else if (buffHealth.maxHealth < buffHealth.health + hill)
            {
                buffHealth.health += buffHealth.maxHealth - buffHealth.health;
            }
            buffHealth.ImageFill();
            ObjectManagment.Destroy(gameObject, anim);
        }
    }
}
