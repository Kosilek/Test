using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

    public Image bar;
    public float hp;
    public float dmg;
    public float fill;

    private void Start()
    {
        fill = 1f;
    }

    private void Update()
    {
        bar.fillAmount = fill;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            DamageCharacter();
    }

    public void DamageCharacter()
    {
        hp -= dmg;
        fill = (hp / dmg);
    }
}
