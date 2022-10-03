using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private GameObject corpsePrefab;

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
                SceneManager.LoadScene("LoseScene");
            }

            if (CompareTag("Enemy"))
            {
                var corpse = Instantiate(corpsePrefab, transform.position, Quaternion.identity);
                corpse.transform.Rotate(Vector3.forward, new System.Random().Next(360));
            }
            Destroy(gameObject);
        }

        public float Health => health;
    }
}