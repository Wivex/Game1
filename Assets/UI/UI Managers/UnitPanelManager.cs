using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitPanelManager : MonoBehaviour
{
    [Header("Unit")]
    protected Unit unit;
    public Image unitImage;
    public TextMeshProUGUI unitName, attack, defence, speed, hazardResist, bleedResist, health, mana, init;
    public Slider healthBar, manaBar, initBar;
    public FloatingText floatingTextPrefab;

    protected virtual void Update()
    {
        attack.text = $"A: {unit.stats[(int)StatType.Attack].BaseValue}";
        defence.text = $"D: {unit.stats[(int)StatType.Defence].BaseValue}";
        speed.text = $"S: {unit.stats[(int)StatType.Speed].BaseValue}";
        hazardResist.text = $"HR: {unit.stats[(int)StatType.HResist].BaseValue}";
        bleedResist.text = $"BR: {unit.stats[(int)StatType.BResist].BaseValue}";

        healthBar.value = (float)unit.stats[(int)StatType.Health].curValue / unit.stats[(int)StatType.Health].BaseValue;
        health.text = $"{unit.stats[(int)StatType.Health].curValue} / {(unit.stats[(int)StatType.Health] as StatChanging).maxValue}";
        manaBar.value = (float)unit.stats[(int)StatType.Mana].curValue / unit.stats[(int)StatType.Mana].BaseValue;
        mana.text = $"{unit.stats[(int)StatType.Mana].curValue} / {(unit.stats[(int)StatType.Mana] as StatChanging).maxValue}";
        initBar.value = unit.curInitiative / Unit.reqInitiative;
        init.text = $"{(int)unit.curInitiative} / {Unit.reqInitiative}";
    }
}