using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

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

        protected virtual void Die()
        {
            if (CompareTag("Player"))
            {
                SceneManager.LoadScene("MenuScene");
            }
            Destroy(gameObject);
        }

        public float Health => health;
    }
}