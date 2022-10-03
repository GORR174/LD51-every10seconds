using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public int strength = 2;
    public float speed = 1;
    
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * speed) * strength);
    }
}
