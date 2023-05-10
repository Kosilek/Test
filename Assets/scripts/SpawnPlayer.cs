using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SpawnPlayer : MonoBehaviour
{

    private void Awake()
    {
        Instantiate(LevelManager.playerSave, transform.position, transform.rotation);
    }
}
