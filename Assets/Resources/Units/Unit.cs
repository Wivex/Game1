using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

    public const int reqInitiative = 100;
    public float curInitiative;

    public abstract UnitPanelManager Panel { get; }
    public bool Dead => stats[(int) StatType.Health].curValue <= 0;

    public abstract void SetStats();

    public void TakeDamage(Damage damage)
    {
        stats[(int) StatType.Health].curValue = Math.Max(stats[(int)StatType.Health].curValue - damage.amount, 0); ;
        var floatingText = Object.Instantiate(Panel.floatingTextPrefab, Panel.unitImage.transform);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();
        textObject.text = $"-{damage.amount}";
    }
}