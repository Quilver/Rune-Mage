using UnityEngine;
namespace SpellSystem
{
    public abstract class SpellController<T> : MonoBehaviour where T : SpellData
    {
        public abstract void InitiateSpell(Data.Projectile data, Transform caster, Vector2 position, Vector2 direction);
        public abstract void ReleaseSpell();
    }
    public class Projectile: SpellController<Data.Projectile>
    {
        public override void InitiateSpell(Data.Projectile data, Transform caster, Vector2 position, Vector2 direction)
        {
            throw new System.NotImplementedException();
        }
        public override void ReleaseSpell()
        {
        }
}
}