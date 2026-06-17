using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "ConeSpell", menuName = "Spell/ConeSpell")]
    public class Cone : SpellData, IRange, ISpeed, IDuration
    {
        [Header("Cone template")]
        float range, duration, speed, arcAngle;
        public LayerMask layerMask;
        public float ArcAngle => arcAngle;

        public float Speed => speed;

        public float Range => range;

        public float Duration => duration;
    }

}
