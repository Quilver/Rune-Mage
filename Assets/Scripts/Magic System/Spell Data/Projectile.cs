using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Spell/Projectile")]
    public class Projectile : SpellData
    {
        [Header("Projectile Data")]
        [SerializeField, Range(0.5f, 10)]
        float _range;
        public float Range=> _range;
        [SerializeField, Range(0.5f, 10)] 
        float _speed;
        public float Speed=> _speed;

    }
}