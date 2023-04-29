using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagment : MonoBehaviour
{
    public static ObjectManagment instance = null;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void Destroy(GameObject gameObject, Animator anim)
    {
        anim.SetBool("Destroy", true);
        Destroy(gameObject, 0.5f);
    }
}
