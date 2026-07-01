using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(UIDocument))]
public class HUD_Controller : MonoBehaviour
{
    public UIDocument ui;
    public PlayerControls playerControls;
    [SerializeField]
    SpriteRenderer fire, earth, wind, lightning;
    [SerializeField]
    SpriteRenderer[] slots;
    private void Awake()
    {
        ui = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        playerControls.GetComponent<Character.HP>().OnHpChanged += GetPlayerHealth;
        playerControls.onRuneAdded += AddRune;
        playerControls.onRuneUsed += RuneUses;
        playerControls.onSpellCast += CastSpell;
        playerControls.OnRuneUnlock += UnlockRune;
        GetPlayerHealth(playerControls.GetComponent<Character.HP>().hp);
    }
    int maxHP = 25;
    void GetPlayerHealth(int hp)
    {
        float HP = (hp*100)/(float)maxHP;
        var hpElement = ui.rootVisualElement.Q<VisualElement>("HP");
        var bgSize = hpElement.style.backgroundSize;    // copy
        bgSize.value = new BackgroundSize(new Length(HP, LengthUnit.Percent), new Length(100, LengthUnit.Percent));      // mutate copy (keeps your existing constructor/usage)
        hpElement.style.backgroundSize = bgSize;
    }
    void AddRune(Rune rune, int spellSlot)
    {
        if (slots == null || slots.Length <= spellSlot)
            return;
        slots[spellSlot].enabled = true;
        slots[spellSlot].sprite = rune.runeIcon;
        /*
        var runeSlot = ui.rootVisualElement.Q("spellSlot");//.AtIndex(spellSlot);
        // convert Sprite -> Background -> StyleBackground
        if(runeSlot == null)
        {
            Debug.LogError($"No spell slot found at index {spellSlot}");
            return;
        }
        if (rune != null)
            runeSlot.style.backgroundImage = new StyleBackground { value = Background.FromSprite(rune.runeIcon) };
        else
            runeSlot.style.backgroundImage = new StyleBackground(); // clear
        */

    }
    void CastSpell()
    {
        foreach (var slot in slots)
            slot.enabled = false;
    }
    void UnlockRune(Rune rune)
    {
        switch (rune.runeType)
        {
            case RuneType.Wind:
                wind.enabled = true;
                break;
            case RuneType.Fire:
                fire.enabled = true;
                break;
            case RuneType.Earth:
                earth.enabled = true;
                break;
            case RuneType.Lightning:
                lightning.enabled = true;
                break;
            default:
                break;
        }
    }
    void RuneUses(Rune rune, int usesLeft)
    {
        switch (rune.runeType)
        {
            case RuneType.Wind:
                ui.rootVisualElement.Q<VisualElement>("Wind").Q<Label>().text = usesLeft.ToString();
                break;
            case RuneType.Fire:
                ui.rootVisualElement.Q<VisualElement>("Fire").Q<Label>().text = usesLeft.ToString();
                break;
            case RuneType.Earth:
                ui.rootVisualElement.Q<VisualElement>("Earth").Q<Label>().text = usesLeft.ToString();   
                break;
            case RuneType.Lightning:
                ui.rootVisualElement.Q<VisualElement>("Lightning").Q<Label>().text = usesLeft.ToString();
                break;
            default:
                break;
        }
    }
}
