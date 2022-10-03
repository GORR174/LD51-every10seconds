using UnityEngine;
using UnityEngine.U2D;
using Util;

namespace Weapon
{
    public class PlayerGun : Gun
    {
        [SerializeField] private Transform shootPoint;

        [SerializeField] private Transform handTransform;
        [SerializeField] private CameraRotator cameraRotator;
        [SerializeField] private SpriteRenderer[] noise;
        [SerializeField] private PixelPerfectCamera camera;

        private float timer;
        private bool canShoot = true;

        public new void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
                timer = 0;
                canShoot = !canShoot;

                if (!canShoot)
                {
                    cameraRotator.strength = 5;
                    cameraRotator.speed = 1.5f;
                    foreach (var spriteRenderer in noise)
                    {
                        spriteRenderer.color = Color.red;
                    }
                }
                else
                {
                    cameraRotator.strength = 2;
                    cameraRotator.speed = 1;
                    foreach (var spriteRenderer in noise)
                    {
                        spriteRenderer.color = Color.white;
                    }
                }
            }
            isShooting = Input.GetMouseButton(0) && canShoot;
            base.Update();
        }

        public override void Shoot()
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