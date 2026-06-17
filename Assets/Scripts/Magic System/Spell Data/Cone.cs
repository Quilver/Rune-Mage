using UnityEngine;
namespace SpellSystem.Data
{
    [CreateAssetMenu(fileName = "ConeSpell", menuName = "Spell/ConeSpell")]
    public class Cone : ScriptableObject
    {
        [Header("Cone template")]
        public float Range, Duration, Speed, ArcAngle;

    }

}
