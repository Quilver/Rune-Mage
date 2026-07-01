using UnityEngine;
namespace SpellSystem
{
    public abstract class SpellCast : MonoBehaviour
    {
        public abstract void Cast(SpellData spellData, Character.TargetSystem caster, Vector2 position, Vector2 direction);
        public virtual void ReleaseSpell()
        {

        }
    }
    public abstract class SpellController<T> : SpellCast where T : SpellData
    {
        [SerializeField]
        protected T data;
        [SerializeField]
        protected Character.TargetSystem caster;
        public override void Cast(SpellData spellData, Character.TargetSystem caster, Vector2 position, Vector2 direction)
        {
            Debug.Assert(spellData != null && typeof(T) == spellData.GetType());
            CastSpell(spellData as T, caster, position, direction);
        }
        public void CastSpell(T data, Character.TargetSystem caster, Vector2 position, Vector2 direction)
        {
            this.data = data;
            Debug.Assert(caster != null);
            this.caster = caster;
            InitiateSpell(position, direction);
        }
        protected virtual void ApplyEffects(GameObject target)
        {
            foreach (var effect in data._effects)
            {
                effect.ApplyEffect(target, caster.gameObject);
            }
        }
        protected abstract void InitiateSpell(Vector2 position, Vector2 direction);
    }
}