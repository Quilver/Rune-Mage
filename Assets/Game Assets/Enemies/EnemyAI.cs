using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Stats")]
    public float moveSpeed = 4f;
    public float attackRange = 3f;
    public float attackCooldown = 1.5f;
    public float stunDuration = 2f;

    private NavMeshAgent agent;
    private bool isStunned = false;
    private float nextAttackTime = 0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //NavMesh.CalculatePath(transform.position, player.position, NavMesh.AllAreas, new NavMeshPath());
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (agent == null || player == null)
        {
            Debug.LogError("EnemyAI: Missing Agent or Player reference!");
            enabled = false;
            return;
        }
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
        agent.SetDestination(player.position);
        nextAttackTime = Time.time;
    }

    void Update()
    {
        if (isStunned) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // Move toward player
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            // Stop and attack
            agent.isStopped = true;
            if (Time.time >= nextAttackTime)
            {
                FireProjectile();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || firePoint
            == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        // Rotate projectile to face player (adjust axis based on your sprite orientation)
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, direction);
        var projectile = Instantiate(projectilePrefab);
        projectile.GetComponent<MagicSystem.Spell.ISpellTarget>().CastSpell(firePoint.position, direction, gameObject);
    }

    void Stun()
    {
        isStunned = true;
        agent.isStopped = true;
        spriteRenderer.color = Color.gray;
        StartCoroutine(ResumeFromStun());
    }

    System.Collections.IEnumerator ResumeFromStun()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        agent.isStopped = false;
        spriteRenderer.color = Color.white;
        agent.SetDestination(player.position);
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
