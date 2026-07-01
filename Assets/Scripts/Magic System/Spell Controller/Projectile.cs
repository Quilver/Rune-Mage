using UnityEngine;

namespace SpellSystem.Controller
{
    [RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : SpellController<Data.Projectile>
    {
        protected override void InitiateSpell(Vector2 position, Vector2 direction)
        {
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