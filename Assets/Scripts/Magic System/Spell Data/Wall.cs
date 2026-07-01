using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "Wall", menuName = "Spell/Wall")]
    public class Wall : SpellData, IRange, IDuration
    {
        [Header("Wall Properties")]
        [SerializeField, Range(1, 100)]
        int _hp;
        public int HP => _hp;
        [SerializeField, Range(0.3f, 10.0f)]
        float _duration;
        public float Duration => _duration;
        [SerializeField, Range(0.4f, 3f)]
        float _range;
        public float Range => _range;
    }
}