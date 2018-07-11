using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public Stat damage;
    public Stat health;
    public UnitStats stats;
    public Dictionary<int, UnitStats> levelStats;

    public void Equip(EquipmentData item)
    {
        //damage.AddModifier(item.statModifiers[])
    }


    //public void TakeDamage(int damage)
    //{
    //    curHealth -= damage;
    //    var floatingText = Instantiate(AM.floatingTextPrefab, transform);
    //    floatingText.GetComponent<TextMeshProUGUI>().text = $"-{damage}";
    //}
}