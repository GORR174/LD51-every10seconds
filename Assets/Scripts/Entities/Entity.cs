﻿using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float health;

        public virtual void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}