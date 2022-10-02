using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    
    private void Update()
    {
        transform.position = Vector3.Lerp(playerTransform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition),
            0.2f);
    }
}