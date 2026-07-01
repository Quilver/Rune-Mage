using UnityEngine;
namespace SpellSystem
{
    public class Wall : SpellController<Data.Wall>
    {
        [SerializeField, Range(1f, 100f)] float distance, duration =100;
        [SerializeField] Vector2 size;
        protected override void InitiateSpell(Vector2 position, Vector2 direction)
        {
            transform.position = position + direction.normalized * distance;
            transform.up = direction;
            Destroy(gameObject, duration);
        }

        public override void ReleaseSpell()
        {
            throw new System.NotImplementedException();
        }

        void onGizmosSelected()
        {

        }
    }
}