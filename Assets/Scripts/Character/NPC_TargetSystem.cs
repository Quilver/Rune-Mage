using UnityEngine;
namespace Character
{
    public class NPC_TargetSystem: TargetSystem
    {
        [SerializeField]
        Transform firePoint;
        [SerializeField] Transform player;
        [SerializeField]
        bool leftOrRight;
        public override Vector2 ShootPosition => firePoint.position;

        public override Vector2 ShootDirection => (player.position - firePoint.position).normalized;
    }
}
