using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
[RequireComponent(typeof(HP))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    SpellFactory spellFactory;
    [SerializeField]
    GameObject wand;
    public Action<int> onHealthChanged;

    [Range(1, 10)]public float moveSpeed = 5f;
    public RuneType[] spellBuffer;
    public Rune wind, fire, earth, lightning;
    public Action<Rune, int> onRuneAdded;
    public Action<Rune, int> onRuneUsed;
    private void Awake()
    {
        GetComponent<HP>().hp = playerData.maxHealth;
        //moveSpeed = playerData.moveSpeed;
        spellBuffer = new RuneType[playerData.maxRuneSlots];
        wind = playerData.windRune;
        wind.UsesLeft = wind.MaxUses;
        fire = playerData.fireRune;
        fire.UsesLeft = fire.MaxUses;
        earth = playerData.earthRune;
        earth.UsesLeft = earth.MaxUses;
        lightning = playerData.lightningRune;
        lightning.UsesLeft = lightning.MaxUses;
    }
    int currentRuneIndex = 0;
    public bool AddRune(Rune rune)
    {
        if(currentRuneIndex < spellBuffer.Length) {
            spellBuffer[currentRuneIndex] = rune.runeType;
            onRuneAdded?.Invoke(rune, currentRuneIndex);
            currentRuneIndex++;
            rune.UsesLeft--;
            onRuneUsed?.Invoke(rune, rune.UsesLeft);
            return true;
        }
        return false;
    }
    public bool CastSpell(bool projection)
    {
        if(currentRuneIndex == 0) return false;
        var spell = Instantiate(spellFactory.GetSpell(spellBuffer.ToList(), projection));
        Debug.Log("Casting spell:" + spell.name);
        spell.Cast(gameObject, wand.transform.position, (wand.transform.position-transform.position).normalized);
        spellBuffer = new RuneType[spellBuffer.Length];
        currentRuneIndex = 0;
        return true;
    }
}
