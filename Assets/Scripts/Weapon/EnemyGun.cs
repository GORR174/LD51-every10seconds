using UnityEngine;
using Util;

namespace Weapon
{
    public class EnemyGun : Gun
    {
        [SerializeField] private Transform shootPoint;

        public new void Update()
        {
            isShooting = Input.GetKey(KeyCode.Space);
            base.Update();
        }
        
        public override void Shoot()
        {
            var instance = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, rb.rotation) * projectileSpeed;
            instance.damage = damage;
            instance.shooterTag = shooterTag;
        }
    }
}