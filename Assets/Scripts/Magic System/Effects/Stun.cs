using UnityEngine;

[System.Serializable]
public class Stun : ISpellEffect
{
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        HP hp = target.GetComponent<HP>();
        if(hp==null) return;
        hp.OnStun?.Invoke();
    }
}
