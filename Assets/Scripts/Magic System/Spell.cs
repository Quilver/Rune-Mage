using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    [SerializeReference]
    MagicSystem.Spell.ISpellTarget target;
    [SerializeReference]
    List<ISpellEffect> effects;

    public void Cast(GameObject caster, Vector2 start, Vector2 direction)
    {
        var target = Instantiate(this.target);
        target.onContactTarget += (GameObject target) =>
        {
            foreach (var effect in effects)
            {
                effect.ApplyEffect(target, caster);
            }
        };
        target.CastSpell(start, direction, caster);
    }
}
