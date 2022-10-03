using System;
using AI;
using Entities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Weapon
{
    public class Projectile : MonoBehaviour
    {
        [HideInInspector] public float damage;
        [HideInInspector] public string shooterTag;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.CompareTag("Enemy") || other.CompareTag("Player"))  
                && !other.CompareTag(shooterTag) || other.GetComponent<TilemapCollider2D>() != null)
            {
                var entity = other.GetComponent<Entity>();
                if (entity != null)
                    entity.TakeDamage(damage);

                Destroy(gameObject);
            }
        }
    }
}