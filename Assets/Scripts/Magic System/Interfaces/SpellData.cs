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
        
    }
    public abstract class SpellDecorator : SpellData
    {
        SpellData spellData;
        public void Decorate(SpellData spellData) { this.spellData = spellData; }
        
    }
}
