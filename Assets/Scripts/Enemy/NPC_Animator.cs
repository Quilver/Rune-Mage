using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
[RequireComponent(typeof(Animator))]
public class NPC_Animator : MonoBehaviour
{
    [System.Serializable]
    struct AnimationState
    {
        [SerializeField]
        public GameObject sprite;
        public void OnEnter()
        {
            sprite.SetActive(true);
         
        }
        public void OnExit()
        {
            sprite.SetActive(false);
        }
    }
    Animator animator;
    [SerializeField]
    GameObject outfit;
    [SerializeField]
    AnimationState Move, Attack, AttackCooldown, Stun, current;
    [SerializeField]
    SpriteRenderer Head;
    SpriteRenderer[] body;
    NavMeshAgent agent;
    private void Start()
    {
        Move.OnEnter();
        Attack.OnExit();
        AttackCooldown.OnExit();
        Stun.OnExit();
        if(outfit != null)
        {
            Instantiate(outfit, transform);
        }
        animator = GetComponent<Animator>();
        body = GetComponentsInChildren<SpriteRenderer>();
        NPC.EnemyAI ai = GetComponentInParent<NPC.EnemyAI>();
        agent = GetComponentInParent<NavMeshAgent>();
        current = Move;
        ai.OnMove += () => { current.OnExit(); Move.OnEnter(); current = Move; };
        ai.Stunned += () => { current.OnExit(); Stun.OnEnter(); current = Stun; };
        ai.OnAttack += () => { current.OnExit(); Attack.OnEnter(); current = Attack; };
        ai.OnAttackCooldown += () => { current.OnExit(); AttackCooldown.OnEnter(); current = AttackCooldown; };
        
    }
    private void Update()
    {
        var velocity = agent.velocity;
        if (velocity.magnitude > 0.01f && !agent.isStopped)
        {
            bool movingRight = velocity.x > 0;
            Moving(movingRight);
            animator.SetBool("Moving", true);
            animator.speed = velocity.magnitude/4;
        }
        else
            animator.SetBool("Moving", false);
        Looking(GetComponentInParent<NPC.EnemyAI>().LookRight());
        
    }
    void Moving(bool right)
    {
        foreach (var item in body)
            item.flipX = right;
        foreach (var item in current.sprite.GetComponentsInChildren<SpriteRenderer>())
            item.flipX = right;
    }
    void Looking(bool right)
    {
        if (current.sprite == Stun.sprite) return;
        Head.flipX = right;
        foreach (var item in current.sprite.GetComponentsInChildren<SpriteRenderer>())
            item.flipX = right;
    }


}
