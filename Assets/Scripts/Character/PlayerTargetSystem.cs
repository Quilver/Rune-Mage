using UnityEngine;
namespace Character
{
    public class PlayerTargetSystem : TargetSystem
    {
        [SerializeField]
        Transform wand;
        public override Vector2 ShootPosition => wand.position;

        public override Vector2 ShootDirection => (wand.position-transform.position).normalized;
    }
}
