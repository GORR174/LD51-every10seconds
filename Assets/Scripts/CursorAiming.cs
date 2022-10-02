using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAiming : MonoBehaviour
{
    [SerializeField] private Transform arm;

    void Start()
    {
    }

    void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10; //The distance between the camera and object
        var armPosition = Camera.main.WorldToScreenPoint(arm.position);
        mousePosition.x -= armPosition.x;
        mousePosition.y -= armPosition.y;
        var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        var scale = AngleToScale(angle);
        transform.localScale = new Vector3(scale, 1, 1);
    //    Debug.Log($"{scale}, {transform.localScale.x}");
        
        arm.rotation = Quaternion.Euler(new Vector3(0, 0, scale < 0 ? -(180 - angle) : angle));
    }

    private int AngleToScale(float angle)
    {
        var cos = Mathf.Cos(Mathf.Deg2Rad * angle);

        if (cos <= -0.1)
            return -1;
        
        return 1;
    }
}