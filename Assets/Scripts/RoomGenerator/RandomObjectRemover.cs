using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectRemover : MonoBehaviour
{
    public float chance;
    
    void Start()
    {
        var rnd = new System.Random().NextDouble();
        
        if (rnd > chance)
        {
            Destroy(gameObject);
        }
    }
}
