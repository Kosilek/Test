using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Health : MonoBehaviour
{
    public Animator anim;
    public float health;
    public float damage;
    [SerializeField]Image _image;
    [SerializeField] float fill = 1f;

    private void Update()
    {
        _image.fillAmount = fill;
    }

    public void TakeDamage(float damage, GameObject gameObject, Animator anim)
    {
        health -= damage;
        fill = (health / 100);
        if (health > 0)
        {
            anim.SetBool("Hit", true);
           Invoke("NotHit", 0.6f);
        }
        if (health <= 0)
        {
            Death(gameObject, anim);
        }
    }

    private void Death(GameObject gameObject, Animator anim)
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        anim.SetInteger("State", 7);
        Destroy(gameObject, 1f);
    }

   private void NotHit()
    {
        anim.SetBool("Hit", false);
    }
}
