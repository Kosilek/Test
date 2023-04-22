using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLevel : MonoBehaviour
{
    private GameObject mainCamera;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        mainCamera.SetActive(false);
    }
}
