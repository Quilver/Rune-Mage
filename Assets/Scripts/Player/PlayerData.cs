using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Range(10, 30)] 
    public int maxHealth;
    [Range(1, 9)]
    public int maxRuneSlots;

    public Rune windRune, fireRune, earthRune, lightningRune;

}
