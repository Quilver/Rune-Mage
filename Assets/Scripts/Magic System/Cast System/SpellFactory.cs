using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace SpellSystem
{
    [CreateAssetMenu(fileName = "SpellFactory", menuName = "Scriptable Objects/SpellFactory")]
    public class SpellFactory : ScriptableObject
    {
        public List<SpellRecipe> spellRecipes;
        public SpellData GetSpell(List<RuneType> runes, bool projected)
        {
            foreach (SpellRecipe recipe in spellRecipes)
            {
                if (!runes.Except(recipe.requirements).Any() && recipe.projected == projected)
                {
                    return recipe.spell;
                }
            }
            Debug.Log("No spell found for the given runes and projection.");
            return null;
        }
    }
    [System.Serializable]
    public struct SpellRecipe
    {
        public SpellData spell;
        public int priority;
        public List<RuneType> requirements;
        public bool projected;//if true, the spell will be cast in the direction the caster is facing, otherwise it will be cast on the caster

    }
}
