using System;
using UnityEngine;
using UnityEngine.AI;
namespace NPC
{
    public class EnemyAI : MonoBehaviour
    {

        [Header("References")]
        public Transform player;
        //public GameObject projectilePrefab;
        public EnemyAttack spear;
        public Transform firePoint;
        public virtual Transform FirePoint
        {
            get
            {
                float x = LookRight() ? -MathF.Abs(firePoint.position.x) : MathF.Abs(firePoint.position.x);
                firePoint.position = new Vector3(x, firePoint.position.y);
                return firePoint;   
            }
        }

        [Header("Stats")]
        public float moveSpeed = 4f;
        public float attackRange = 3f;
        public float attackWarmup = 0.5f;
        public float attackCooldown = 1.5f;
        public float stunDuration = 2f;
        // Events for animation and other systems
        public Action OnMove, OnAttack, OnAttackCooldown, Stunned, OnSwapSides, Spot;


        NavMeshAgent _agent;
        protected NavMeshAgent Agent
        {
            get
            {
                if (_agent != null) return _agent;
                _agent = GetComponent<NavMeshAgent>();
                Agent.updatePosition = false;
                Agent.updateRotation = false;
                Agent.updateUpAxis = false;
                Agent.speed = moveSpeed;
                Agent.SetDestination(player.position);
                return _agent;
            }
        }
        protected bool isStunned = false;
        protected bool isAttacking = false;
        protected float nextAttackTime = 0f;
        protected SpriteRenderer spriteRenderer;

        protected virtual void Start()
        {
            //NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, new NavMeshPath());

            //agent.updatePosition = false;
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (Agent == null || player == null)
            {
                Debug.LogError("EnemyAI: Missing Agent or Player reference!");
                enabled = false;
                return;
            }
            nextAttackTime = Time.time;
            GetComponent<Character.HP>().OnStun += Stun;
            enabled = false;
        }
        private void OnEnable()
        {
            if (_agent != null)
                Spot?.Invoke();
            Agent.enabled = true;
        }
        private void OnDisable()
        {
            Agent.enabled = false;
            StopAllCoroutines();
        }

        protected virtual void Update()
        {
            if (isStunned || isAttacking) return;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                // Move toward player
                Agent.isStopped = false;
                Agent.ResetPath();
                var rb = GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Agent.desiredVelocity, Time.deltaTime);
                //GetComponentInChildren<FrictionJoint2D>().GetComponent<Rigidbody2D>().linearVelocity = agent.desiredVelocity;
                Agent.SetDestination(player.position);
                OnMove?.Invoke();
            }
            else
            {
                StartCoroutine(MakeAttack());
            }
        }
        public bool LookRight()
        {
            return transform.position.x < player.position.x;
        }
        protected void Attack()
        {
            
            if (FirePoint == null) return;

            Vector2 direction = (player.position - transform.position).normalized;
            // Rotate projectile to face player (adjust axis based on your sprite orientation)
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, direction);
            spear.attack.Cast(GetComponent<Character.TargetSystem>(), FirePoint.position, direction);
        }

        void Stun()
        {
            isStunned = true;
            Agent.isStopped = true;
            StartCoroutine(ResumeFromStun());
            Stunned?.Invoke();
        }

        System.Collections.IEnumerator ResumeFromStun()
        {
            yield return new WaitForSeconds(stunDuration);
            isStunned = false;
            Agent.isStopped = false;
            Agent.SetDestination(player.position);
        }

        System.Collections.IEnumerator MakeAttack()
        {
            Agent.isStopped = true;
            isAttacking = true;
            OnAttack?.Invoke();
            yield return new WaitForSeconds(attackWarmup);
            Attack();
            OnAttackCooldown?.Invoke();
            yield return new WaitForSeconds(attackWarmup);
            Agent.isStopped = false;
            Agent.SetDestination(player.position);
            nextAttackTime = Time.time + attackCooldown;
            isAttacking = false;
        }
        void Die()
        {
            // Add death animation/sfx here
            Destroy(gameObject);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }

}
