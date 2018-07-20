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
    public TacticsPreset tacticsPreset;

    public const int reqInitiative = 100;
    public float curInitiative;
    public string name;

    public abstract UnitPanelManager Panel { get; }
    public bool Dead => stats[(int) StatType.Health].curValue <= 0;

    public abstract void SetStats();
    public abstract void SetAbilities();

    public void TakeDamage(Damage damage)
    {
        stats[(int) StatType.Health].curValue = Math.Max(stats[(int)StatType.Health].curValue - damage.amount, 0); ;
        var floatingText = Object.Instantiate(Panel.floatingTextPrefab, Panel.unitImage.transform);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();
        textObject.text = $"-{damage.amount}";
    }
}