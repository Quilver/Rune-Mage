using UnityEngine;

[System.Serializable]
public class Heal : ISpellEffect
{
    [SerializeField, Range(1, 10)] int healAmount;
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        var hp = target.GetComponent<HP>();
        if (hp == null) return;
        hp.Heal(healAmount);
        Debug.Log($"Heal applied to {target.name} by {caster.name}");
        
    }
}
