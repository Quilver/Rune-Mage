using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "SelfCast", menuName = "Spell/Self")]
    public class SelfCast : SpellData
    {
        [Header("Effect Data")]
        [SerializeField, Range(0f, 10f), Tooltip("How long you can cast the spell for, 0 is an instant effect")]
        float _duration = 0;
        
    }
}