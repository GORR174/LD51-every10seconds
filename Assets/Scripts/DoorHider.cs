using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHider : MonoBehaviour
{
    //  public SpriteRenderer top;
    //  public SpriteRenderer bottom;
    private Color doorColor = new Color(1, 1, 1, 1);
    [SerializeField] private SpriteRenderer SpriteRenderer;

    public void MakeDoorHidden()
    {
        SpriteRenderer.color = Color.gray;
        // top.color = Color.white;
        //  bottom.color = Color.white;
    }

    public void UnhideDoor()
    {
        SpriteRenderer.color = doorColor;
        //  top.color = doorColor;
        //  bottom.color = doorColor;
    }
}