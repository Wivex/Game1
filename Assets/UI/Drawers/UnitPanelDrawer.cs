using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanelDrawer : Drawer
{
    protected Unit unit;

    protected Canvas canvas;
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

    Image[] effects,
        abilities;

    TextMeshProUGUI[] effectsDur,
        abilitiesCD;

    protected virtual void OnValidate()
    {
        canvas = GetComponent<Canvas>();

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
        //// update stats
        //attack.text = $"ATT: {ColoredStat(unit.baseStats[(int) StatType.Attack])}";
        //defence.text = $"DEF: {ColoredStat(unit.baseStats[(int) StatType.Defence])}";
        //speed.text = $"SPD: {ColoredStat(unit.baseStats[(int) StatType.Speed])}";
        //hazardResist.text = $"HRES: {ColoredStat(unit.baseStats[(int) StatType.HResist])}";
        //bleedResist.text = $"BRES: {ColoredStat(unit.baseStats[(int) StatType.BResist])}";

        //// update stat bars
        //healthBar.value = (float) unit.baseStats[(int) StatType.Health].curValue /
        //                  unit.baseStats[(int) StatType.Health].curValue;
        //health.text =
        //    $"{unit.baseStats[(int) StatType.Health].curValue} / {(unit.baseStats[(int) StatType.Health] as StatChanging).maxValue}";
        //manaBar.value = (float) unit.baseStats[(int) StatType.Energy].curValue /
        //                unit.baseStats[(int) StatType.Energy].curValue;
        //mana.text =
        //    $"{unit.baseStats[(int) StatType.Energy].curValue} / {(unit.baseStats[(int) StatType.Energy] as StatChanging).maxValue}";
        //initBar.value = unit.curInitiative / Unit.reqInitiative;
        //initiative.text = $"{(int) unit.curInitiative} / {Unit.reqInitiative}";

        // update effects
        for (var i = 0; i < effects.Length; i++)
        {
            if (i >= unit.effects.Count)
            {
                effects[i].sprite = null;
                effects[i].color = Color.clear;
                effectsDur[i].text = string.Empty;
            }
            else
            {
                //effects[i].sprite = unit.effects[i].icon;
                effects[i].color = Color.white;
                effectsDur[i].text = unit.effects[i].curDuration.ToString();
            }
        }

        // update abilities
        for (var i = 0; i < abilities.Length; i++)
        {
            if (i >= unit.abilities.Count)
            {
                abilities[i].sprite = null;
                abilities[i].color = Color.clear;
                abilitiesCD[i].text = string.Empty;
            }
            else
            {
                abilities[i].sprite = unit.abilities[i].data.icon;
                abilities[i].color = Color.white;
                abilitiesCD[i].text = unit.abilities[i].curCooldown != 0
                    ? unit.abilities[i].curCooldown.ToString()
                    : string.Empty;
            }
        }
    }

    string ColoredStat(int value)
    {
        //if (stat.curValue < stat.BaseValue) return $"<color=\"red\">{stat.curValue}</color>";
        //if (stat.curValue > stat.BaseValue) return $"<color=\"green\">{stat.curValue}</color>";
        return $"{value}";
    }
}