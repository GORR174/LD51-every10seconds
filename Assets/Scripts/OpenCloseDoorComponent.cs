using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoorComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            // doorAnimator.SetBool("IsOpen", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            // doorAnimator.SetBool("IsOpen", false);
        }
    }
}