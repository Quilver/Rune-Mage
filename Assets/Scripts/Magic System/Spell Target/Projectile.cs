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

            Collider2D casterCol = caster.GetComponent<Collider2D>();
            if (casterCol != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), casterCol);
            }
        }
        private void Update()
        {
            transform.up = GetComponent<Rigidbody2D>().linearVelocity;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == gameObject.layer) return;   
            onContactTarget?.Invoke(collision.gameObject);
            Destroy(gameObject);
        }
        void onGizmosSelected()
        {

        }
    }
}