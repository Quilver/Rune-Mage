using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "Beam", menuName = "Spell/Beam")]
    public class Beam : SpellData
    {
        [Header("Beam Data")]
        [SerializeField, Range(3f, 100f)] float duration;
        [SerializeField, Range(1f, 100f)] float maxRange, speed;
        [SerializeField, Range(0.1f, 10f)] float width;
    }

}
