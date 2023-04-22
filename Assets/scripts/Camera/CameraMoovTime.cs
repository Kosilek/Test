using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMoovTime : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    private void Update()
    {
        transform.position = new Vector3(transform.position.x + cameraSpeed, transform.position.y, transform.position.z);
    }
}
