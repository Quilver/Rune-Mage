using UnityEngine;
namespace MagicSystem.Target
{
    public class Wall : Spell.ISpellTarget
    {
        [SerializeField, Range(1f, 100f)] float distance, duration =100;
        [SerializeField] Vector2 size;
        public override void CastSpell(Vector2 position, Vector2 direction, GameObject caster)
        {
            transform.position = position + direction.normalized * distance;
            transform.up = direction;
            Destroy(gameObject, duration);
        }
        void onGizmosSelected()
        {

        }
    }
}