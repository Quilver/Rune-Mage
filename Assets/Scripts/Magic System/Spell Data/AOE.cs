using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Spell/AOE")]
    public class AOE : SpellData, IRange, ISpeed
    {
        [Header("AOE Data")]
        [SerializeField, Range(0.6f, 10f)] float radius;
        [SerializeField, Range(0.1f, 3f)] float secondsToFullRadius, secondsAtMaxRadius;
        public float Range => radius;
        public float Speed => 1f/secondsToFullRadius;
    }
}