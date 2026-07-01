using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttack", menuName = "Scriptable Objects/EnemyAttack")]
public class EnemyAttack : ScriptableObject
{
    public string attackName;
    public float range;
    public float attackWarmup;
    public float attackCooldown;
    public SpellSystem.SpellData attack; // Optional override
    public bool requirePathCheck;
}
