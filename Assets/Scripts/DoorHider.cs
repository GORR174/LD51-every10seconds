using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHider : MonoBehaviour
{
    private readonly Color doorColor = new Color(1, 1, 1, 1);
    [SerializeField] private SpriteRenderer SpriteRenderer;

    public void MakeDoorHidden()
    {
        SpriteRenderer.color = Color.gray;
    }

    public void UnhideDoor()
    {
        SpriteRenderer.color = doorColor;
    }
}