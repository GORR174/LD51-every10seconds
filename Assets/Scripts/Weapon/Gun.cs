using System;
using System.Collections;
using UnityEngine;
using Util;

namespace Weapon
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private int shootingSpeed;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;

        private string shooterTag;
        private bool isShooting;
        private Coroutine currentShootingCoroutine;

        private void Start()
        {
            shooterTag = transform.parent.tag;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                isShooting = true;

            if (Input.GetMouseButtonUp(0))
                isShooting = false;

            if (isShooting)
            {
                currentShootingCoroutine = StartCoroutine(ShootPeriodically());
            }
            else if (currentShootingCoroutine != null)
            {
                StopCoroutine(currentShootingCoroutine);
                currentShootingCoroutine = null;
            }
        }

        private IEnumerator ShootPeriodically()
        {
            while (true)
            {
                SpawnProjectile();
                yield return new WaitForSeconds(shootingSpeed);
            }
        }

        public virtual void SpawnProjectile()
        {
            var instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, rb.rotation) * projectileSpeed;
            instance.damage = damage;
            instance.shooterTag = shooterTag;
        }
    }
}