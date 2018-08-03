using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class Unit
{
    [Header("Unit")]
    public Stat[] stats = {
        new StatChanging(),
        new StatChanging(),
        new Stat(),
        new Stat(),
        new Stat(),
        new Stat(),
        new Stat(),
    };

    public List<Ability> abilities = new List<Ability>();
    public List<Effect> curEffects = new List<Effect>();
    public TacticsPreset tacticsPreset;

    public const int reqInitiative = 100;
    public float curInitiative;
    public string name;

    public abstract UnitPanelDrawer unitPanel { get; }
    public bool Dead => stats[(int)StatType.Health].curValue <= 0;

    public abstract void SetStats();
    public abstract void SetAbilities();

    public int TakeDamage(Damage damage)
    {
        var protectionValue = 0;
        switch (damage.type)
        {
            case DamageType.Physical:
                protectionValue = stats[(int)StatType.Defence].curValue;
                break;
            case DamageType.Hazardous:
                protectionValue = stats[(int)StatType.HResist].curValue;
                break;
            case DamageType.Bleeding:
                protectionValue = stats[(int)StatType.BResist].curValue;
                break;
        }
        var healthLoss= Math.Max(damage.amount - protectionValue, 0);
        stats[(int)StatType.Health].curValue = Math.Max(stats[(int)StatType.Health].curValue - healthLoss, 0);

        var floatingText = Object.Instantiate(unitPanel.floatingTextPrefab, unitPanel.unitImage.transform);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();
        textObject.text = $"-{healthLoss}";
        textObject.color = Color.red;

        return healthLoss;
    }

    public void Heal(int amount)
    {
        stats[(int)StatType.Health].curValue = Mathf.Min(stats[(int)StatType.Health].curValue + amount, (stats[(int)StatType.Health] as StatChanging).maxValue);

        var floatingText = Object.Instantiate(unitPanel.floatingTextPrefab, unitPanel.unitImage.transform);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();
        textObject.text = $"+{amount}";
        textObject.color = Color.green;
    }
}