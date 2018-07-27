using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanelManager : MonoBehaviour
{
    [Header("Unit")] protected Unit unit;
    public Image unitImage;

    public Transform effectsPanel,
        abilitiesPanel;

    public TextMeshProUGUI unitName,
        attack,
        defence,
        speed,
        hazardResist,
        bleedResist,
        health,
        mana,
        initiative;

    Image[] effects,
        abilities;

    TextMeshProUGUI[] effectsDur,
        abilitiesCD;

    public Slider healthBar, manaBar, initBar;
    public FloatingText floatingTextPrefab;

    protected virtual void OnValidate()
    {
        effects = effectsPanel.gameObject.GetComponentsInChildren<Image>();
        effectsDur = effectsPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        abilities = abilitiesPanel.gameObject.GetComponentsInChildren<Image>();
        abilitiesCD = abilitiesPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
    }

    protected virtual void Update()
    {
        // update stats
        attack.text = $"A: {ColoredStat(unit.stats[(int)StatType.Attack])}";
        defence.text = $"D: {ColoredStat(unit.stats[(int) StatType.Defence])}";
        speed.text = $"S: {ColoredStat(unit.stats[(int) StatType.Speed])}";
        hazardResist.text = $"HR: {ColoredStat(unit.stats[(int) StatType.HResist])}";
        bleedResist.text = $"BR: {ColoredStat(unit.stats[(int) StatType.BResist])}";

        // update stat bars
        healthBar.value = (float) unit.stats[(int) StatType.Health].curValue /
                          unit.stats[(int) StatType.Health].curValue;
        health.text =
            $"{unit.stats[(int) StatType.Health].curValue} / {(unit.stats[(int) StatType.Health] as StatChanging).maxValue}";
        manaBar.value = (float) unit.stats[(int) StatType.Mana].curValue /
                        unit.stats[(int) StatType.Mana].curValue;
        mana.text =
            $"{unit.stats[(int) StatType.Mana].curValue} / {(unit.stats[(int) StatType.Mana] as StatChanging).maxValue}";
        initBar.value = unit.curInitiative / Unit.reqInitiative;
        initiative.text = $"{(int) unit.curInitiative} / {Unit.reqInitiative}";

        // update effects
        for (var i = 0; i < unit.curEffects.Count; i++)
        {
            effects[i].sprite = unit.curEffects[i].icon;
            effectsDur[i].text = unit.curEffects[i].curDuration.ToString();
        }

        // update effects
        for (var i = 0; i < unit.abilities.Count; i++)
        {
            abilities[i].sprite = unit.abilities[i].abilityData.icon;
            abilitiesCD[i].text = unit.abilities[i].curCooldown.ToString();
        }
    }

    string ColoredStat(Stat stat)
    {
        if (stat.curValue < stat.BaseValue) return $"<color=\"red\">{stat.curValue}</color>";
        if (stat.curValue > stat.BaseValue) return $"<color=\"green\">{stat.curValue}</color>";
        return $"{stat.curValue}";
    }
}