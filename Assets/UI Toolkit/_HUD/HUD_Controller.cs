using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(UIDocument))]
public class HUD_Controller : MonoBehaviour
{
    public UIDocument ui;
    public PlayerControls playerControls;


    private void Awake()
    {
        ui = GetComponent<UIDocument>();
    }
    private void OnEnable()
    {
        playerControls.onHealthChanged += GetPlayerHealth;
        playerControls.onRuneAdded += AddRune;
        playerControls.onRuneUsed += RuneUses;
        GetPlayerHealth(playerControls.GetComponent<HP>().hp);
    }
    void GetPlayerHealth(int hp)
    {
        ui.rootVisualElement.Q<ProgressBar>("HP_Bar").value = hp;
    }
    void AddRune(Rune rune, int spellSlot)
    {
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
