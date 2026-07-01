using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName ="HitScan", menuName = "Spell/HitScan")]
    public class HitScan : SpellData, IRange
    {

        [Header("Hitscan")]
        [SerializeField, Range(0.3f, 10f)]
        float _range;
        public float Range => _range;
    }
}