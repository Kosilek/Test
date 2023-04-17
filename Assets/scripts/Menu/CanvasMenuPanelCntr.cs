using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenuPanelCntr : MonoBehaviour
{
    [SerializeField] GameObject[] Panel;

    private void Awake()
    {
        Panel[0].SetActive(true);
        UnActiv();
    }

    private void UnActiv()
    {
        for (int i = 1; i < 4; i++)
        {
            Panel[i].SetActive(false);
        }
    }
}
