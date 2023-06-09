using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public int count = 5;
    public void AddCoin()
    {
        Event.SendScoreCoin(count);
        ObjectManagment.Destroy(gameObject, anim);
    }
}
