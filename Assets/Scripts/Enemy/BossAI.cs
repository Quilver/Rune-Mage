using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : EnemyAI
{
    [Header("Boss Attacks")]
    public List<EnemyAttack> attacks = new List<EnemyAttack>();
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
            agent.isStopped = false;
            agent.SetDestination(player.position);
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
        agent.isStopped = true;
        isAttacking = true;
        OnAttack?.Invoke();

        yield return new WaitForSeconds(currentAttack.attackWarmup);

        // Friendly Fire Check
        if (checkFriendlyFire && currentAttack.requirePathCheck)
        {
            if (!CanFireAtTarget(player.position, out Vector3 direction, out Transform firePoint))
            {
                Debug.Log("[BossAI] Attack blocked by ally, delaying shot.");
                yield return new WaitForSeconds(currentAttack.attackWarmup * 0.5f);

                // Retry once after delay
                if (!CanFireAtTarget(player.position, out direction, out firePoint))
                {
                    isAttacking = false;
                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                    yield break;
                }
            }
            FireProjectileAt(direction, firePoint);
        }
        else
        {
            Transform firePoint = LookRight() ? rightFirePoint : leftFirePoint;
            // Fallback to base logic
            bool lookRight = transform.position.x < player.position.x;
            firePoint = lookRight ? rightFirePoint : leftFirePoint;
            Vector3 direction = (player.position - transform.position).normalized;
            FireProjectileAt(direction, firePoint);
        }

        OnAttackCooldown?.Invoke();
        yield return new WaitForSeconds(currentAttack.attackCooldown);

        agent.isStopped = false;
        agent.SetDestination(player.position);
        isAttacking = false;
    }

    private bool CanFireAtTarget(Vector3 targetPos, out Vector3 direction, out Transform firePoint)
    {
        direction = (targetPos - transform.position).normalized;
        bool lookRight = transform.position.x < player.position.x;
        firePoint = lookRight ? rightFirePoint : leftFirePoint;

        if (firePoint == null) return false;

        Vector3 toTarget = (targetPos - firePoint.position).normalized;
        float distanceToTarget = Vector3.Distance(firePoint.position, targetPos);

        // Raycast to detect allies in the firing line
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, toTarget, distanceToTarget, allyLayer);
        return hit.collider == null;
    }

    private void FireProjectileAt(Vector3 direction, Transform firePoint)
    {
        
        //var prefabToUse = currentAttack.attack != null ? currentAttack.attack : projectilePrefab;
        //if (prefabToUse == null || firePoint == null) return;
        currentAttack.attack.Cast(gameObject, firePoint.position, player.position- firePoint.position);
    }

    [SerializeField]
    GameObject Victory;
    public void OnDestroy()
    {
        Victory.SetActive(true);
    }
}
