using MagicSystem;
using UnityEngine;
namespace Character
{
    public abstract class TargetSystem : MonoBehaviour
    {
        public abstract Vector2 ShootPosition { get; }
        public abstract Vector2 ShootDirection { get; }
    }
}
