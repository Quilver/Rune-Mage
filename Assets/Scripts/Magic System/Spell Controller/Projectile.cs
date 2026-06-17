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
namespace SpellSystem.Controller
{
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : SpellController<Data.Projectile>
    {
        Data.Projectile data;
        Transform caster;
        public override void InitiateSpell(Data.Projectile data, Transform caster, Vector2 position, Vector2 direction)
        {
            this.data = data;
            this.caster = caster;
            transform.position = position;
            transform.up = direction;
            GetComponent<Rigidbody2D>().linearVelocity = data.Speed * direction;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == gameObject.layer) return;
            //onContactTarget?.Invoke(collision.gameObject);
            foreach (var effect in data._effects)
            {
                effect.ApplyEffect(collision.gameObject, caster.gameObject);
            }
            Destroy(gameObject);
        }
        public override void ReleaseSpell()
        {
            
        }
    }
}