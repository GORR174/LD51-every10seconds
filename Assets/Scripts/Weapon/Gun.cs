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
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;

        [SerializeField] private Transform shootPoint;

        [SerializeField] private Transform handTransform;

        private string shooterTag;
        private bool isShooting;

        private float cooldown;

        private void Start()
        {
            shooterTag = transform.parent.tag;
        }

        private void Update()
        {
            isShooting = Input.GetMouseButton(0);

            if (isShooting)
                ShootPeriodically();

            cooldown -= Time.deltaTime * 10;
            Debug.Log(cooldown);
        }

        private void ShootPeriodically()
        {
            if (cooldown <= 0)
            {
                SpawnProjectile();
                cooldown = shootingSpeed;
            }

            /*while (true)
            {
                SpawnProjectile();
                yield return new WaitForSeconds(shootingSpeed);
            }*/
        }

        public virtual void SpawnProjectile()
        {
            var rotation = handTransform.rotation;
            var instance = Instantiate(projectilePrefab, shootPoint.position,
                Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y,
                    transform.localScale.x < 0 ? rotation.eulerAngles.z + 90 : rotation.eulerAngles.z - 90));
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, rb.rotation) * projectileSpeed;
            instance.damage = damage;
            instance.shooterTag = shooterTag;
        }
    }
}