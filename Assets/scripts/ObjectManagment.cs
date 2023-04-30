using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagment : Singletone<ObjectManagment>
{

    protected override void Awake()
    {
        base.Awake();
    }

    public static void Destroy(GameObject gameObject, Animator anim)
    {
        anim.SetBool(MeaningString.destroy, true);
        Destroy(gameObject, 0.5f);
    }
}
