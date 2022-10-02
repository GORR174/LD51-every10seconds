using UnityEngine;
using Util;

namespace Weapon
{
    public class PlayerGun : Gun
    {
        [SerializeField] private Transform shootPoint;

        [SerializeField] private Transform handTransform;

        public new void Update()
        {
            isShooting = Input.GetMouseButton(0);
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