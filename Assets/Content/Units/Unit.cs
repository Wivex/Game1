using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
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