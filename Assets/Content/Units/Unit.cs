using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{
    [Header("Unit")]
    public UnitStats curStats;
    public UnitStats maxStats;

    public void ModifyStat(StatType type, StatModifier mod)
    {
        curStats = new UnitStats(maxStats);
    }

    //public abstract void SetStats();

    //public void TakeDamage(int damage)
    //{
    //    curHealth -= damage;
    //    var floatingText = Instantiate(AM.floatingTextPrefab, transform);
    //    floatingText.GetComponent<TextMeshProUGUI>().text = $"-{damage}";
    //}
}