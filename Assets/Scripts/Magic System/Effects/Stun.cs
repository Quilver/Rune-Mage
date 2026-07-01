using UnityEngine;

[System.Serializable]
public class Stun : ISpellEffect
{
    public override void ApplyEffect(GameObject target, GameObject caster)
    {
        Character.HP hp = target.GetComponent<Character.HP>();
        if(hp==null) return;
        hp.OnStun?.Invoke();
    }
}
