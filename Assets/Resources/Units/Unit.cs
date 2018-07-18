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

    public abstract void SetStats();

    public void TakeDamage(Damage damage)
    {
        var floatingText = Object.Instantiate(Panel.floatingTextPrefab, Panel.unitImage.transform);
        var textObject = floatingText.GetComponent<TextMeshProUGUI>();
        textObject.text = $"-{damage.amount}";
    }

    public abstract UnitPanelManager Panel { get; }
}