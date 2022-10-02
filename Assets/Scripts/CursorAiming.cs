using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAiming : MonoBehaviour
{
    [SerializeField] private Transform arm;

    void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10; //The distance between the camera and object
        var armPosition = Camera.main.WorldToScreenPoint(arm.position);
        mousePosition.x -= armPosition.x;
        mousePosition.y -= armPosition.y;
        var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        
        if (Mathf.Abs(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - arm.position.x) > 0.27f)
        {
            transform.localScale = mousePosition.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }

        arm.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x < 0 ? -(180 - angle) : angle));
    }
}