using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SpellSystem.Controller
{
    public class Cone : SpellController<Data.Cone>
    {
        [SerializeField, Range(2, 20)] int rayCount = 12;
        ParticleSystem particle;
        protected override void InitiateSpell(Vector2 position, Vector2 direction)
        {
            transform.position = caster.ShootPosition;
            transform.up = direction;
            particle = GetComponentInChildren<ParticleSystem>();
            particle.startSpeed = data.Speed;
            particle.startLifetime = data.Range/data.Speed;
            
        }
        void FixedUpdate()
        {
            if (data == null) return;
            Debug.Assert(caster != null);
            
            transform.position = caster.ShootPosition;
            transform.up = caster.ShootDirection;
            foreach (var target in GetTargets(data.Range))
            {
                ApplyEffects(target);
            }
        }
        public override void ReleaseSpell()
        {
            Destroy(gameObject);
        }
        List<GameObject> GetTargets(float range)
        {
            List<GameObject> targets = new();
            float deltaAngle = data.ArcAngle / rayCount;

            for (int i = 0; i < rayCount; i++)
            {
                Vector3 ray = RotateDir(deltaAngle, caster.ShootDirection);
                var hit = Physics2D.Raycast(caster.ShootPosition, ray, range, data.Layer);
                if (hit)
                {
                    targets.Add(hit.collider.gameObject);
                }
            }

            return targets;
        }

        Vector3 RotateDir(float angle, Vector3 dir)
        {
            return Quaternion.Euler(0, 0, angle) * dir;
        }
        private void OnDrawGizmos()
        {
            if (data == null) return;
            Gizmos.color = Color.black;
            float deltaAngle = data.ArcAngle / rayCount;

            for (int i = 0; i < rayCount; i++)
            {
                Vector3 ray = RotateDir(data.ArcAngle/2 - deltaAngle*i, transform.up);
                var hit = Physics2D.Raycast(transform.position, ray, data.Range, data.Layer);
                Gizmos.DrawRay(caster.ShootPosition, data.Range * ray);
            }
        }
    }

}
/*
namespace MagicSystem.Target
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Cone : Spell.ISpellTarget
    {
        [SerializeField, Range(1f, 20f)] float maxRange, duration, speed;
        [SerializeField, Range(1f, 120f)] float angle;
        [SerializeField, Range(2, 20)] int rayCount = 12;
        Mesh mesh;
        Transform caster;
        [SerializeField]
        LayerMask layerMask;
        Vector3 forward, position;
        void Start()
        {

            //StartCoroutine(ScaleAnimation());
        }
        IEnumerator ScaleAnimation()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            mesh = CreateMesh();
            MeshFilter filter = GetComponent<MeshFilter>();
            filter.mesh = mesh;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                forward = (caster.position - caster.parent.position).normalized;
                position = caster.position;

                transform.GetComponentInChildren<ParticleSystem>().transform.position = position;
                transform.GetComponentInChildren<ParticleSystem>().transform.right = forward;

                elapsed += Time.deltaTime; // Track time passed
                float range = Mathf.Min(maxRange, elapsed * speed);
                GetTargets(range);
                filter.mesh.vertices = UpdateMeshVertices(mesh, range);
                filter.mesh.RecalculateNormals();
                filter.mesh.RecalculateBounds();
                yield return new WaitForFixedUpdate(); // Wait for the next frame
            }
            gameObject.SetActive(false);
            //OnRelease();
        }
        List<Transform> GetTargets(float range)
        {
            List<Transform> targets = new();
            for (int i = 0; i < rayCount; i++)
            {
                float angle = -this.angle + (i * (this.angle * 2 / rayCount));
                Vector3 direction = RayDir(angle, forward);
                RaycastHit2D hit = Physics2D.Raycast(position, direction, range, layerMask);
                if (hit.collider != null && !targets.Contains(hit.collider.transform))
                {
                    targets.Add(hit.collider.transform);
                    onContactTarget?.Invoke(hit.collider.gameObject);
                }
            }
            return targets;
        }
        #region Mesh
        Vector3 RayDir(float angle, Vector3 up)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            return rotation * up;
        }
        Mesh CreateMesh()
        {
            Mesh mesh = new();
            Vector3[] vertices = new Vector3[rayCount + 2];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];
            vertices[0] = position;
            int vertexIndex = 1;
            int triangleIndex = 0;

            for (int i = 0; i < rayCount; i++)
            {
                vertices[vertexIndex] = RayDir(-angle + (i * (angle * 2 / rayCount)), transform.up);
                if (i > 0)
                {
                    triangles[triangleIndex] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;
                }
                vertexIndex++;
                triangleIndex += 3;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            return mesh;
        }


        Vector3[] UpdateMeshVertices(Mesh mesh, float range)
        {

            Vector3[] verts = mesh.vertices;
            verts[0] = position;
            for (int i = 1; i < rayCount + 1; i++)
            {
                float angle = -this.angle + (i * (this.angle * 2 / rayCount));
                Vector3 direction = RayDir(angle, forward);
                RaycastHit2D hit = Physics2D.Raycast(position, direction, range, LayerMask.GetMask("Default"));
                if (hit.collider != null)
                {
                    // convert world hit point to local-space vertex
                    verts[i] = hit.point;
                }
                else
                {
                    // local-space point at distance 'range' along the local direction
                    verts[i] = position + direction * range;
                }
            }
            return verts;
        }


        #endregion

        public override void CastSpell(Vector2 position, Vector2 direction, GameObject caster)
        {
            this.caster = caster.transform.GetComponentInChildren<Wand>().transform;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            forward = direction;
            this.position = position;
            caster.GetComponent<PlayerControls>().onSpellRelease = OnRelease;
            StartCoroutine(ScaleAnimation());
        }
        void OnRelease()
        {
            if (gameObject == null) return;
            //Debug.Log("Spell Released");
            //caster.gameObject.GetComponent<PlayerControls>().onSpellRelease -= OnRelease;
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
*/