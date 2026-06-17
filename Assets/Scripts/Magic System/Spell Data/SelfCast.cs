using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "SelfCast", menuName = "Spell/Self")]
    public class SelfCast : SpellData, IDuration
    {
        [Header("Effect Data")]
        [SerializeField, Range(0f, 10f), Tooltip("How long you can cast the spell for, 0 is an instant effect")]
        float duration = 0;

        public float Duration => duration;
    }
}