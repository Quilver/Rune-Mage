using UnityEngine;
namespace SpellSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class Hitscan : SpellController<Data.HitScan>
    {
        public override void ReleaseSpell()
        {
            throw new System.NotImplementedException();
        }

        protected override void InitiateSpell(Vector2 position, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        /*
        public override void CastSpell(Vector2 position, Vector2 direction, GameObject caster)
        {
            //transform.position = position;
            //transform.up = direction;
            float distance = range;
            GameObject target = null;
            var targets = Physics2D.OverlapCircleAll(position, range, LayerMask.GetMask("NPC"));
            foreach (var hit in targets)
            {
                if (hit.gameObject != caster && Vector2.Distance(position, hit.transform.position) < distance)
                {
                    target = hit.gameObject;
                    distance = Vector2.Distance(position, hit.transform.position);
                }
            }
            GameObject.Destroy(gameObject, 0.3f);
            if (target == null) return;
            onContactTarget?.Invoke(target);
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, position);
            lineRenderer.SetPosition(1, target.transform.position);
        }
        */
        void onGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, data.Range);
        }
    }
}