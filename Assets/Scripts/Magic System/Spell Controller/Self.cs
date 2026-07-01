using UnityEngine;
namespace SpellSystem.Controller
{
    public class SelfCast : SpellController<Data.SelfCast>
    {
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