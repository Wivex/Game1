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

    public Transform unitDetailsIcon, unitPreviewIcon;
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

        CreateFloatingText(unitDetailsIcon, -healthLoss);
        CreateFloatingText(unitPreviewIcon, -healthLoss);

        return healthLoss;
    }

    public void Heal(int amount)
    {
        stats[(int)StatType.Health].curValue = Mathf.Min(stats[(int)StatType.Health].curValue + amount, (stats[(int)StatType.Health] as StatChanging).maxValue);

        CreateFloatingText(unitDetailsIcon, amount);
        CreateFloatingText(unitPreviewIcon, amount);
    }

    void CreateFloatingText(Transform target, int value)
    {
        var floatingText = Object.Instantiate(UIManager.instance.floatingTextPrefab, target);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();

        if (value > 0)
        {
            textObject.text = $"+{value}";
            textObject.color = Color.green;
        }
        if (value < 0)
        {
            textObject.text = $"{value}";
            textObject.color = Color.red;
        }
    }
}