using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;
using Weapon;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AI
{
    public class EnemyAI : MonoBehaviour
    {
        private Transform player;
        private EnemyGun gun;

        private AIDestinationSetter destinationSetter;
        private AIPath aiPath;

        public RadiusTrigger chaisePlayerTrigger;
        public RadiusTrigger attackPlayerTrigger;

        public GameObject walkPointPrefab;

        // Patroling
        private Transform walkPoint;
        private bool isWalkPointSet;
        public float walkPointRange;

        // Attacking
        public float attackCooldown;
        private bool alreadyAttacked;

        //States 
        public bool playerInSightRange, playerInAttackRange;

        private void Awake()
        {
            gun = GetComponent<EnemyGun>();
            player = Object.FindObjectOfType<PlayerMovement>().transform;
            destinationSetter = GetComponent<AIDestinationSetter>();
            aiPath = GetComponent<AIPath>();
        }

        private void Start()
        {
            chaisePlayerTrigger.onTriggerEnter += c =>
            {
                if (c.CompareTag("Player"))
                    playerInSightRange = true;
            };

            chaisePlayerTrigger.onTriggerExit += c =>
            {
                if (c.CompareTag("Player"))
                    playerInSightRange = false;
            };

            attackPlayerTrigger.onTriggerEnter += c =>
            {
                if (c.CompareTag("Player"))
                    playerInAttackRange = true;
            };

            attackPlayerTrigger.onTriggerExit += c =>
            {
                if (c.CompareTag("Player"))
                    playerInAttackRange = false;
            };
        }

        private void Update()
        {
            if (!playerInSightRange && !playerInAttackRange)
                Patroling();
            
            if (playerInSightRange && !playerInAttackRange)
                ChaisePlayer();

            if (playerInAttackRange && playerInSightRange)
                AttackPlayer();
        }

        private void Patroling()
        {
            if (!isWalkPointSet)
                SearchWalkPoint();

            if (isWalkPointSet)
                destinationSetter.target = walkPoint;

            if (aiPath.reachedDestination)
                isWalkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            if (walkPoint != null)
                Destroy(walkPoint.gameObject);

            var randomY = Random.Range(-walkPointRange, walkPointRange);
            var randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = Instantiate(walkPointPrefab,
                transform.position + new Vector3(randomX, randomY, 0),
                Quaternion.identity).GetComponent<Transform>();
            isWalkPointSet = true;
        }

        private void ChaisePlayer()
        {
            destinationSetter.target = player.transform;
        }

        private void AttackPlayer()
        {
            destinationSetter.target = transform;

            if (!alreadyAttacked)
            {
                Debug.Log("Attacking");
                gun.isShooting = true;
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), attackCooldown);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
            gun.isShooting = false;
        }
    }
}