using MagicSystem.Spell;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
namespace MagicSystem
{
    public class Effects : MonoBehaviour
    {
        [SerializeField]
        List<ISpellEffect> effects = new List<ISpellEffect>();
        [SerializeField]
        ISpellTarget target;
        private void Awake()
        {
            foreach (var effect in effects)
            {
                target.onContactTarget += (GameObject target) => effect.ApplyEffect(target, gameObject);
            }
    }
    }
}