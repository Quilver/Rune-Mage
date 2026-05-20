using UnityEngine;

[CreateAssetMenu(fileName = "Rune", menuName = "Scriptable Objects/Rune")]
public class Rune : ScriptableObject
{
    public Sprite runeIcon;
    public RuneType runeType;
    [Range(1, 24)]
    public int MaxUses;
    [Range(1, 24)]
    public int UsesLeft;
    public bool unlocked = false;
}
public enum RuneType
{
    Wind,
    Fire,
    Earth,
    Lightning
}