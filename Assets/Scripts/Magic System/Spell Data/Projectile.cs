using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Spell/Projectile")]
    public class Projectile : SpellData, ICollider, IRange, ISpeed
    {
        [Header("Projectile Data")]
        [SerializeField, Range(0.5f, 10)]
        float _range;
        public float Range=> _range;
        [SerializeField, Range(0.5f, 10)] 
        float _speed;
        public float Speed=> _speed;

        [SerializeField] LayerMask _layermask;
        public LayerMask Layer => _layermask;
        [SerializeField] bool _isTrigger = true;
        public bool TriggerOrCollider => _isTrigger;
    }
}