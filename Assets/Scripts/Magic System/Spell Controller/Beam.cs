using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpellSystem
{
    [RequireComponent(typeof(LineRenderer))]
    public class Beam : SpellController<Data.Beam>
    {
        [SerializeField, Range(3f, 100f)] float duration;
        [SerializeField, Range(1f, 100f)] float maxRange, speed;
        [SerializeField, Range(0.1f, 10f)] float width;
        
        LineRenderer lineRenderer;
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            StartCoroutine(ScaleAnimation());
        }
        IEnumerator ScaleAnimation()
        {
            float elapsed = 0f;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime; // Track time passed
                float range = Mathf.Min(maxRange, elapsed * speed);
                
                lineRenderer.SetPosition(1, transform.up* range);

                yield return null; // Wait for the next frame
            }
            gameObject.SetActive(false);
        }

        protected override void InitiateSpell(Vector2 position, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public override void ReleaseSpell()
        {
            throw new System.NotImplementedException();
        }
    }
}
