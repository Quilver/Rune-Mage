using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    [Header("References")]
    public Transform player;
    public GameObject projectilePrefab;
    public Transform leftFirePoint, rightFirePoint;

    [Header("Stats")]
    public float moveSpeed = 4f;
    public float attackRange = 3f;
    public float attackWarmup = 0.5f;
    public float attackCooldown = 1.5f;
    public float stunDuration = 2f;
    // Events for animation and other systems
    public Action OnMove, OnAttack, OnAttackCooldown, Stunned, OnSwapSides, Spot;


    NavMeshAgent _agent;
    protected NavMeshAgent agent
    {
        get
        {
            if(_agent!=null)return _agent;
            _agent = GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = moveSpeed;
            agent.SetDestination(player.position);
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

        if (agent == null || player == null)
        {
            Debug.LogError("EnemyAI: Missing Agent or Player reference!");
            enabled = false;
            return;
        }
        nextAttackTime = Time.time;
        GetComponent<HP>().OnStun += Stun;
        enabled = false;
    }
    private void OnEnable()
    {
        if (_agent != null)
            Spot?.Invoke();
        agent.enabled = true;
    }
    private void OnDisable()
    {
        agent.enabled = false;
        StopAllCoroutines();
    }

    protected virtual void Update()
    {
        if (isStunned ||isAttacking) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // Move toward player
            agent.isStopped = false;
            agent.ResetPath();
            var rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, agent.desiredVelocity, Time.deltaTime);
            //GetComponentInChildren<FrictionJoint2D>().GetComponent<Rigidbody2D>().linearVelocity = agent.desiredVelocity;
            agent.SetDestination(player.position);
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
        Transform firePoint = LookRight() ? rightFirePoint : leftFirePoint;
        if (projectilePrefab == null || firePoint == null) return;

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
        StartCoroutine(ResumeFromStun());
        Stunned?.Invoke();
    }

    System.Collections.IEnumerator ResumeFromStun()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    System.Collections.IEnumerator MakeAttack()
    {
        agent.isStopped = true;
        isAttacking = true;
        OnAttack?.Invoke();
        yield return new WaitForSeconds(attackWarmup);
        Attack();
        OnAttackCooldown?.Invoke();
        yield return new WaitForSeconds(attackWarmup);
        agent.isStopped = false;
        agent.SetDestination(player.position);
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
