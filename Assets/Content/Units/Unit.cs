using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
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

    public void Start()
    {
        SetStats();
    }

    //public void TakeDamage(int damage)
    //{
    //    curHealth -= damage;
    //    var floatingText = Instantiate(AM.floatingTextPrefab, transform);
    //    floatingText.GetComponent<TextMeshProUGUI>().text = $"-{damage}";
    //}
}