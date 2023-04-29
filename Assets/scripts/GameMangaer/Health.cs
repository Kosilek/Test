using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class Health : MonoBehaviour
{
    public Animator anim;
    public float maxHealth;
    public float health;
    public float damage;
    [SerializeField]Image _image;
    [SerializeField] float fill = 1f;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        ImageFill();
    }

    public void TakeDamage(float damage, GameObject gameObject, Animator anim)
    {
        health -= damage;
        
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

    public void ImageFill()
    {
        _image.fillAmount = fill;
        fill = ((health * 100) / maxHealth) / 100;
    }

    public void Death(GameObject gameObject, Animator anim)
    {
        if (gameObject.GetComponent<DamageObject>())
        {
            gameObject.GetComponent<DamageObject>().damage = 0;
        }
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
