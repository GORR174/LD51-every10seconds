using Unity.VisualScripting;
using UnityEngine;
using Util;

namespace Weapon
{
    public class EnemyGun : Gun
    {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Transform playerTransform;
        
        
        public new void Update()
        {
            base.Update();
        }
        
        public override void Shoot()
        {
            LookAtPlayer();
            var instance = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, shootPoint.rotation.eulerAngles.z + (transform.localScale.x < 0 ? 90 : - 90)) * projectileSpeed;
            instance.damage = damage;
            instance.shooterTag = shooterTag;
        }

        private void LookAtPlayer()
        {
            var playerPosition = playerTransform.position;
            var position = transform.position;
            playerPosition.x -= position.x;
            playerPosition.y -= position.y;
            
            var angle = Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg;
        
            if (Mathf.Abs(playerPosition.x - position.x) > 0.27f)
            {
                transform.localScale = playerPosition.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            }

            shootPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.localScale.x < 0 ? -(180 - angle) : angle));
        }
    }
}