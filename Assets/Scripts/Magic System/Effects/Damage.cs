using UnityEngine;
[System.Serializable]
public class Damage : ISpellEffect
{
    [SerializeField, Range(1, 10)] int damage;
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        target.GetComponent<Character.HP>()?.TakeDamage(damage);
        Debug.Log($"Damage applied to {target.name} by {caster.name}");
    }
}
