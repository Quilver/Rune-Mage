using UnityEngine;
using System.Collections;

namespace MagicSystem.Target
{
    [RequireComponent(typeof(ParticleSystem))]
    public class AOE : Spell.ISpellTarget
    {
        ParticleSystem particle;
        [SerializeField, Range(0.6f, 10f)] float radius;
        [SerializeField, Range(0.1f, 3f)] float secondsToFullRadius, secondsAtMaxRadius;

        void Start()
        {
            particle = GetComponent<ParticleSystem>();
            StartCoroutine(ScaleAnimation());


        }
        IEnumerator ScaleAnimation()
        {
            float elapsed = 0f;
            Vector3 startScale = Vector3.zero;
            Vector3 endScale = new Vector3(radius, radius, 1); // Assuming 2D circle
            //particle.duration=secondsToFullRadius+secondsAtMaxRadius;
            //particle.startLifetime = secondsToFullRadius + secondsAtMaxRadius;
            var size = particle.sizeOverLifetime.size;
            while (elapsed < secondsToFullRadius)
            {
                elapsed += Time.deltaTime; // Track time passed

                // Calculate progress (0 to 1)
                float percent = size.Evaluate(elapsed / secondsToFullRadius);

                // Apply the scale
                transform.localScale = Vector3.Lerp(startScale, endScale, percent);

                yield return null; // Wait for the next frame
            }

            // Ensure it hits the exact final size at the end
            transform.localScale = endScale;
            elapsed = 0f;
            yield return new WaitForSeconds(secondsAtMaxRadius);
            gameObject.SetActive(false);
        }

        public override void CastSpell(Vector2 position, Vector2 direction, GameObject caster)
        {
            particle = GetComponent<ParticleSystem>();
            transform.localScale = radius * Vector3.one;
            transform.position = caster.transform.position;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            onContactTarget?.Invoke(collision.gameObject);
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius*0.5f);
        }
    }
}

