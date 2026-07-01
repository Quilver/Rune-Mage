using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace NPC
{
    public class BossAI : EnemyAI
    {
        [Header("Boss Attacks")]
        public List<EnemyAttack> attacks = new();
        public float attackSwapInterval = 10f;

        [Header("Friendly Fire Prevention")]
        public LayerMask allyLayer;
        public bool checkFriendlyFire = true;

        private int currentAttackIndex = -1;
        [SerializeField]
        private EnemyAttack currentAttack;
        private float nextAttackSwapTime;

        protected override void Start()
        {
            base.Start();
            if (attacks == null || attacks.Count == 0)
            {
                Debug.LogError("BossAI: No attacks assigned in inspector!");
                enabled = false;
                return;
            }
            nextAttackSwapTime = Time.time;
            SwapToNextAttack();
        }

        protected override void Update()
        {
            if (isStunned || isAttacking) return;

            // Check if it's time to swap attacks
            if (Time.time >= nextAttackSwapTime)
            {
                SwapToNextAttack();
            }

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > currentAttack.range)
            {
                Agent.isStopped = false;
                Agent.SetDestination(player.position);
                OnMove?.Invoke();
            }
            else
            {
                if (currentAttackIndex >= 0)
                    StartCoroutine(PerformBossAttack());
            }
        }

        private void SwapToNextAttack()
        {
            currentAttackIndex = (currentAttackIndex + 1) % attacks.Count;
            currentAttack = attacks[currentAttackIndex];
            nextAttackSwapTime = Time.time + attackSwapInterval;

            Debug.Log($"[BossAI] Phase switched to: {currentAttack.attackName}");
            OnSwapSides?.Invoke(); // Reuse for VFX/SFX
        }

        private IEnumerator PerformBossAttack()
        {
            Agent.isStopped = true;
            isAttacking = true;
            OnAttack?.Invoke();

            yield return new WaitForSeconds(currentAttack.attackWarmup);

            // Friendly Fire Check
            if (checkFriendlyFire && currentAttack.requirePathCheck)
            {
                if (!CanFireAtTarget(player.position, out Vector3 direction))
                {
                    Debug.Log("[BossAI] Attack blocked by ally, delaying shot.");
                    yield return new WaitForSeconds(currentAttack.attackWarmup * 0.5f);

                    // Retry once after delay
                    if (!CanFireAtTarget(player.position, out direction))
                    {
                        isAttacking = false;
                        Agent.isStopped = false;
                        Agent.SetDestination(player.position);
                        yield break;
                    }
                }
                currentAttack.attack.Cast(GetComponent<Character.TargetSystem>(), FirePoint.position, direction);
            }
            else
            {
                Vector3 direction = (player.position - transform.position).normalized;
                currentAttack.attack.Cast(GetComponent<Character.TargetSystem>(), FirePoint.position, direction);
            }

            OnAttackCooldown?.Invoke();
            yield return new WaitForSeconds(currentAttack.attackCooldown);

            Agent.isStopped = false;
            Agent.SetDestination(player.position);
            isAttacking = false;
        }

        private bool CanFireAtTarget(Vector3 targetPos, out Vector3 direction)
        {
            direction = (targetPos - FirePoint.position).normalized;
            Debug.Assert(FirePoint != null);

            Vector3 toTarget = (targetPos - FirePoint.position).normalized;
            float distanceToTarget = Vector3.Distance(FirePoint.position, targetPos);

            // Raycast to detect allies in the firing line
            RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, toTarget, distanceToTarget, allyLayer);
            return hit.collider == null;
        }

        [SerializeField]
        GameObject Victory;
        public void OnDestroy()
        {
            Victory.SetActive(true);
        }
    }

}
