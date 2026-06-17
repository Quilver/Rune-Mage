using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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
namespace SpellSystem.Controller
{
    public class Cone : SpellController<Data.Cone>
    {
        Data.Cone data;
        Transform caster;
        [SerializeField, Range(2, 20)] int rayCount = 12;
        public override void InitiateSpell(Data.Cone data, Transform caster, Vector2 position, Vector2 direction)
        {
            this.data = data;
            this.caster = caster;

        }

        public override void ReleaseSpell()
        {
            throw new System.NotImplementedException();
        }
        List<GameObject> GetTargets(float range)
        {
            List<GameObject> targets = new List<GameObject>();
            float deltaAngle = data.ArcAngle / rayCount;

            for (int i = 0; i < rayCount; i++)
            {
                Vector3 ray = RayDir(deltaAngle, caster.up);
                var hit = Physics2D.Raycast(caster.position, caster.up, range, data.layerMask);
                if (hit)
                {
                    targets.Add(hit.collider.gameObject);
                }
            }

            return targets;
        }

        Vector3 RayDir(float angle, Vector3 up)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            return rotation * up;
        }
        private void OnDrawGizmos()
        {
            Vector3 leftRay = data.Range * RayDir(-data.ArcAngle/2, caster.up);
            Vector3 rightRay = data.Range * RayDir(data.ArcAngle / 2, caster.up);
            Gizmos.DrawRay(transform.position, leftRay);
            Gizmos.DrawRay(transform.position, rightRay);
        }
    }

}