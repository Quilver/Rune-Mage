using UnityEngine;
namespace MagicSystem.Target
{
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : Spell.ISpellTarget
    {
        [SerializeField, Range(1f, 100f)] float range, speed;
        public override void CastSpell(Vector2 position, Vector2 direction, GameObject caster)
        {
            transform.position = position + direction * 0.5f;
            transform.up = direction;
            GetComponent<Rigidbody2D>().linearVelocity = speed * direction;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            onContactTarget?.Invoke(collision.gameObject);
            Destroy(gameObject);
        }
        void onGizmosSelected()
        {

        }
    }
}