using System;
using UnityEngine;
using UnityEngine.AI;
using Weapon;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AI
{
    public class EnemyAI : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask groundMask, playerMask;

        public EnemyGun gun;

        // Patroling
        public Vector3 walkPoint;
        private bool isWalkPointSet;
        public float walkPointRange;

        // Attacking
        public float attackCooldown;
        private bool alreadyAttacked;

        //States 
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Awake()
        {
            player = Object.FindObjectOfType<PlayerMovement>().transform;
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        private void Update()
        {
            playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, playerMask);
            playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, playerMask);
            
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
                agent.SetDestination(walkPoint);

            var distanceToWalkPoint = transform.position - walkPoint;
            if (distanceToWalkPoint.magnitude < 1)
                isWalkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            var randomY = Random.Range(-walkPointRange, walkPointRange); 
            var randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = transform.position + new Vector3(randomX, randomY, 0);
        }

        private void ChaisePlayer()
        {
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            if (!alreadyAttacked)
            {
                gun.transform.LookAt(player);
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