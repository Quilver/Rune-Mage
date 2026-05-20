using UnityEngine;
[CreateAssetMenu(fileName = "Damage", menuName = "Scriptable Objects/Spell Effect/Damage")]
public class Damage : ISpellEffect
{
    [SerializeField, Range(1, 10)] int damage;
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        target.GetComponent<HP>()?.TakeDamage(damage);
        Debug.Log($"Damage applied to {target.name} by {caster.name}");
    }
}
