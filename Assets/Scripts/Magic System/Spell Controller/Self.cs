using UnityEngine;
namespace MagicSystem.Target
{
    public class Self : Spell.ISpellTarget
    {
        [SerializeField, Range(0, 30)]
        private float duration;

        public override void CastSpell(Vector2 position, Vector2 direction, GameObject caster)
        {
            transform.position = caster.transform.position;
            onContactTarget?.Invoke(caster);
            Destroy(gameObject, duration);
        }
        void onGizmosSelected()
        {

        }
    }
}
