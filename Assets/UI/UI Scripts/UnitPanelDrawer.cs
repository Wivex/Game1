using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanelDrawer : MonoBehaviour
{
    [Header("Unit")]
    protected Unit unit;
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

    public Slider healthBar, manaBar, initBar;
    public FloatingText floatingTextPrefab;

    Image[] effects,
        abilities;

    TextMeshProUGUI[] effectsDur,
        abilitiesCD;

    protected virtual void OnValidate()
    {
        var childImages = new List<Image>();
        effectsPanel.gameObject.GetComponentsInChildren(true, childImages);
        effects = childImages.FindAll(image => image.gameObject.name.Contains("Image")).ToArray();
        effectsDur = effectsPanel.gameObject.GetComponentsInChildren<TextMeshProUGUI>();
        abilitiesPanel.gameObject.GetComponentsInChildren(true, childImages);
        abilities = childImages.FindAll(image => image.gameObject.name.Contains("Image")).ToArray();
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
            effects[i].sprite = unit.curEffects.Count < i ? unit.curEffects[i].icon : null;
            effectsDur[i].text = unit.curEffects.Count < i ? unit.curEffects[i].curDuration.ToString() : string.Empty;
        }

        // update effects
        for (var i = 0; i < unit.abilities.Count; i++)
        {
            abilities[i].sprite = unit.abilities.Count < i ? unit.abilities[i].abilityData.icon : null;
            abilitiesCD[i].text = unit.abilities.Count < i ? unit.abilities[i].curCooldown.ToString() : string.Empty;
        }
    }

    string ColoredStat(Stat stat)
    {
        if (stat.curValue < stat.BaseValue) return $"<color=\"red\">{stat.curValue}</color>";
        if (stat.curValue > stat.BaseValue) return $"<color=\"green\">{stat.curValue}</color>";
        return $"{stat.curValue}";
    }
}