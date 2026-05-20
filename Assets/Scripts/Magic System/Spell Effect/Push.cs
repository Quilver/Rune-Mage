using UnityEngine;

[CreateAssetMenu(fileName = "Push", menuName = "Scriptable Objects/Spell Effect/Push")]
public class Push : ISpellEffect
{
    [SerializeField, Range(.3f, 5)] float pushForce;
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        Debug.Log($"Push applied to {target.name} by {caster.name}");
        var rb = target.GetComponent<Rigidbody2D>();
        rb.AddForce((target.transform.position - caster.transform.position).normalized * rb.mass * pushForce, ForceMode2D.Force);
    }
}
