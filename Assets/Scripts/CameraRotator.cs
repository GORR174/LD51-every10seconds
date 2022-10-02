using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time) * 2);
    }
}
