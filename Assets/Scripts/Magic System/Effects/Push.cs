using UnityEngine;

[System.Serializable]
public class Push : ISpellEffect
{
    [SerializeField, Range(.3f, 5)] float pushForce = 1f;
    [SerializeField, Tooltip("Force for continuous; Impulse for impact.")] ForceMode2D forceMode;
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        var rb = target.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        rb.AddForce((target.transform.position - caster.transform.position).normalized * rb.mass * pushForce, ForceMode2D.Force);
    }
}
