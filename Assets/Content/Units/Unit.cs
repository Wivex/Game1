using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [Header("Unit")]
    public new string name;
    public UnitData unitData;
    public UnitStats curStats;

    void Start()
    {
        SetStats();
    }

    public void Equip(EquipmentData item)
    {
        //damage.AddModifier(item.statModifiers[])
    }

    public void SetStats()
    {
        //curStats = baseStats;
    }


    //public void TakeDamage(int damage)
    //{
    //    curHealth -= damage;
    //    var floatingText = Instantiate(AM.floatingTextPrefab, transform);
    //    floatingText.GetComponent<TextMeshProUGUI>().text = $"-{damage}";
    //}
}