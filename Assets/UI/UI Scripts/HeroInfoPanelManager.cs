using UnityEngine;
using UnityEngine.UI;

public class HeroInfoPanelManager : MonoBehaviour
{
    [HideInInspector]
    public Hero hero;
    
    public Transform backpackPanel,
        effectsPanel,
        abilitiesPanel;

    [HideInInspector]
    public Image[] inventorySlots,
        backpackSlots,
        effects,
        abilities;

    void OnValidate()
    {
        backpackSlots = backpackPanel.gameObject.GetComponentsInChildren<Image>();
        effects = backpackPanel.gameObject.GetComponentsInChildren<Image>();
        abilities = backpackPanel.gameObject.GetComponentsInChildren<Image>();
    }

    void Update()
    {
        UpdateInventoryPanel();
        UpdateBackpackPanel();
        UpdateEffectsPanel();
        UpdateAbilitiesPanel();
    }

    // NOTE: performance?
    void UpdateInventoryPanel()
    {
        for (var i = 0; i < inventorySlots.Length; i++)
            inventorySlots[i].sprite = hero.inventory[i].icon;
    }

    // NOTE: performance?
    void UpdateBackpackPanel()
    {
        for (var i = 0; i < backpackSlots.Length; i++)
            backpackSlots[i].sprite = hero.backpack[i].icon;
    }

    // NOTE: performance?
    void UpdateEffectsPanel()
    {
        for (var i = 0; i < effects.Length; i++)
            effects[i].sprite = hero.curEffects[i].icon;
    }

    // NOTE: performance?
    void UpdateAbilitiesPanel()
    {
        for (var i = 0; i < abilities.Length; i++)
            abilities[i].sprite = hero.abilities[i].abilityData.icon;
    }
}