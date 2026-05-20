using UnityEngine;
[System.Serializable]
public abstract class ISpellEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target, GameObject caster);
}
