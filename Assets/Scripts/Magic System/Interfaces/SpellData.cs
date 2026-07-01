using SerializeReferenceEditor;
using System.Collections.Generic;
using UnityEngine;
namespace SpellSystem
{
    public abstract class SpellData : ScriptableObject
    {
        [Header("General")]
        [SerializeField] string _spellName;
        [SerializeReference, SR] 
        public List<ISpellEffect> _effects;
        [SerializeField]
        GameObject _spellObject;
        public SpellCast Cast(Character.TargetSystem caster, Vector2 position, Vector2 direction)
        {
            Debug.Assert(caster != null);
            var spellInstance = Instantiate(_spellObject);
            SpellCast spell = spellInstance.GetComponent<SpellCast>();
            Debug.Assert(spell != null);
            
            spell.Cast(this, caster, position, direction);
            return spell;
        }


    }
    public interface ICollider
    {
        LayerMask Layer {  get; }
        public bool TriggerOrCollider { get; }
    }
    public interface IRange
    {
        float Range { get; }
    }
    public interface ISpeed
    {
        float Speed { get; }
    }
    public interface IDuration
    {
        float Duration { get; }
    }
    public abstract class SpellDecorator
    {
        SpellData spellData;
        public void Decorate(SpellData spellData) { this.spellData = spellData; }
        
    }
}
