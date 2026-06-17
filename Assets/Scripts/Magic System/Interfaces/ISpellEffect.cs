using UnityEngine;
[System.Serializable]
public abstract class ISpellEffect
{
    public abstract void ApplyEffect(GameObject target, GameObject caster);
}
