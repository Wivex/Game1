using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitPanelManager : MonoBehaviour
{
    [Header("Unit")] protected Unit unit;
    public Image unitImage;

    public TextMeshProUGUI unitName,
        attack,
        defence,
        speed,
        hazardResist,
        bleedResist,
        health,
        mana,
        init;

    public Slider healthBar, manaBar, initBar;
    public FloatingText floatingTextPrefab;

    protected virtual void Update()
    {
        attack.text = $"A: {ColoredStat(unit.stats[(int)StatType.Attack])}";
        defence.text = $"D: {ColoredStat(unit.stats[(int) StatType.Defence])}";
        speed.text = $"S: {ColoredStat(unit.stats[(int) StatType.Speed])}";
        hazardResist.text = $"HR: {ColoredStat(unit.stats[(int) StatType.HResist])}";
        bleedResist.text = $"BR: {ColoredStat(unit.stats[(int) StatType.BResist])}";

        healthBar.value = (float) unit.stats[(int) StatType.Health].curValue /
                          unit.stats[(int) StatType.Health].curValue;
        health.text =
            $"{unit.stats[(int) StatType.Health].curValue} / {(unit.stats[(int) StatType.Health] as StatChanging).maxValue}";
        manaBar.value = (float) unit.stats[(int) StatType.Mana].curValue /
                        unit.stats[(int) StatType.Mana].curValue;
        mana.text =
            $"{unit.stats[(int) StatType.Mana].curValue} / {(unit.stats[(int) StatType.Mana] as StatChanging).maxValue}";
        initBar.value = unit.curInitiative / Unit.reqInitiative;
        init.text = $"{(int) unit.curInitiative} / {Unit.reqInitiative}";
    }

    string ColoredStat(Stat stat)
    {
        if (stat.curValue < stat.BaseValue) return $"<color=\"red\">{stat.curValue}</color>";
        if (stat.curValue > stat.BaseValue) return $"<color=\"green\">{stat.curValue}</color>";
        return $"{stat.curValue}";
    }
}