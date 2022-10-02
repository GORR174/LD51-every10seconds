using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouse.x, mouse.y, 0);
    }
}
