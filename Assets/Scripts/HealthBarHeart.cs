using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class HealthBarHeart : MonoBehaviour
{
    public Entity playerEntity;
    public int hpCount;
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _spriteRenderer.enabled = playerEntity.Health >= hpCount;
    }
}
