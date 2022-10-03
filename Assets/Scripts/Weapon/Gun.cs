using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using Util;

namespace Weapon
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float shootingSpeed;
        [SerializeField] protected Projectile projectilePrefab;
        [SerializeField] protected float damage;
        [SerializeField] protected float projectileSpeed;

        protected string shooterTag;
        public bool isShooting;

        private float cooldown;

        private void Start()
        {
            shooterTag = transform.tag;
        }

        public virtual void Update()
        {
            if (isShooting)
                ShootPeriodically();

            cooldown -= Time.deltaTime * 10;
        }

        private void ShootPeriodically()
        {
            if (cooldown <= 0)
            {
                Shoot();
                cooldown = shootingSpeed;
            }
        }

        public virtual void Shoot()
        {
            
        }
    }
}