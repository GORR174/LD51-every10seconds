using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHider : MonoBehaviour
{
    private readonly Color doorColor = new Color(1, 1, 1, 1);
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    public void MakeDoorHidden()
    {
        spriteRenderer.color = Color.gray;
        boxCollider2D.enabled = true;
    }

    public void UnhideDoor()
    {
        spriteRenderer.color = doorColor;
        boxCollider2D.enabled = false;
    }
}