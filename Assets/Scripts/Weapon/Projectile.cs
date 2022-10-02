using System;
using Entities;
using UnityEngine;

namespace Weapon
{
    public class Projectile : MonoBehaviour
    {
        [HideInInspector] public float damage;
        [HideInInspector] public string shooterTag;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(shooterTag) || other.GetComponent<Projectile>() != null)
                return;

            var entity = other.GetComponent<Entity>();
            if (entity != null)
                entity.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}