using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "AOE", menuName = "Spell/AOE")]
    public class AOE : SpellData
    {
        [Header("AOE Data")]
        [SerializeField, Range(0.6f, 10f)] float radius;
        [SerializeField, Range(0.1f, 3f)] float secondsToFullRadius, secondsAtMaxRadius;

    }
}