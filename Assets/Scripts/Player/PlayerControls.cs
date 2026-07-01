using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.Collections.Unicode;
[RequireComponent(typeof(Character.HP))]
[RequireComponent(typeof(Character.TargetSystem))]
public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;
    public void LevelUp(PlayerData data)
    {
        playerData = data;
        GetComponent<Character.HP>().hp = data.maxHealth;
        Reset();
    }
    public Action<Rune> OnRuneUnlock;
    public void UnlockRune(Rune rune)
    {
        switch (rune.runeType)
        {
            case RuneType.Wind:
                wind.unlocked = true;
                //playerData.windRune.unlocked = true;
                break;
            case RuneType.Fire:
                fire.unlocked = true;
                //playerData.fireRune.unlocked = true;
                break;
            case RuneType.Earth:
                earth.unlocked = true;
                //playerData.earthRune.unlocked = true;
                break;
            case RuneType.Lightning:
                lightning.unlocked = true;
                //playerData.lightningRune.unlocked = true;
                break;
            default:
                break;
        }
        OnRuneUnlock?.Invoke(rune);
    }
    [SerializeField]
    SpellSystem.SpellFactory spellFactory;
    [SerializeField]
    GameObject wand;
    //public Action<int> onHealthChanged;

    [Range(1, 10)]public float moveSpeed = 5f;
    public RuneType[] spellBuffer;
    public Rune wind, fire, earth, lightning;
    public Action<Rune, int> onRuneAdded;
    public Action<Rune, int> onRuneUsed;
    public Action onSpellCast, onSpellRelease;
    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        Debug.Log("Setting player hp:" + playerData.maxHealth);
        GetComponent<Character.HP>().SetHP(playerData.maxHealth);
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

        onRuneUsed?.Invoke(wind, wind.UsesLeft);
        onRuneUsed?.Invoke(fire, fire.UsesLeft);
        onRuneUsed?.Invoke(earth, earth.UsesLeft);
        onRuneUsed?.Invoke(lightning, lightning.UsesLeft);
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
        //var spell = Instantiate(spellFactory.GetSpell(spellBuffer.ToList(), projection));
        //Debug.LogError("Should be casting spell:" + spell.name);
        Vector2 pos = transform.position;
        Vector2 dir = (wand.transform.position - transform.position).normalized;
        spellFactory.GetSpell(spellBuffer.ToList(), projection).Cast(GetComponent<Character.PlayerTargetSystem>(), pos, dir);
        //spell.(gameObject, wand.transform.position, (wand.transform.position-transform.position).normalized);
        spellBuffer = new RuneType[spellBuffer.Length];
        currentRuneIndex = 0;
        onSpellCast?.Invoke();
        return true;
    }
    public void CancelSpell()
    {
        Debug.Log("Spell casting canceled");
        onSpellRelease?.Invoke();
        onSpellRelease = null;
    }
}
