using UnityEngine;
namespace MagicSystem.Target
{
    public class Self : Spell.ISpellTarget
    {
        [SerializeField, Range(1f, 100f)] float range;
        public override void CastSpell(Vector2 position, Vector2 direction, GameObject caster)
        {
            throw new System.NotImplementedException();
        }
        void onGizmosSelected()
        {

        }
    }
}
