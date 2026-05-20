using System;
using UnityEngine;
namespace MagicSystem
{
    namespace Spell
    {
        
        public abstract class ISpellTarget: MonoBehaviour
        {
            public Action<GameObject> onContactTarget;
            public abstract void CastSpell(Vector2 position, Vector2 direction, GameObject caster);

        }
    }
}