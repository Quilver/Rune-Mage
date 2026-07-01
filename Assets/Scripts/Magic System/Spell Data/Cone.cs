using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "ConeSpell", menuName = "Spell/ConeSpell")]
    public class Cone : SpellData, ICollider, IRange, ISpeed, IDuration
    {
        [Header("Cone template")]
        [SerializeField]
        LayerMask layerMask;
        [SerializeField]
        bool _isTrigger = true;
        [SerializeField, Range(0.5f, 10f)]
        float range;
        [SerializeField, Range(0.5f, 10f)]
        float speed;
        [SerializeField, Range(0.5f, 30f)]
        float duration;
        [SerializeField, Range(0f, 180f)]
        float arcAngle;
        
        public float ArcAngle => arcAngle;
        public float Speed => speed;
        public float Range => range;
        public float Duration => duration;
        public LayerMask Layer => layerMask;
        public bool TriggerOrCollider => _isTrigger;
    }

}
