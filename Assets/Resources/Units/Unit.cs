using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;



public abstract class Unit
{
    [Header("Unit")]
    internal StatSheet curStats;
    internal List<Ability> abilities = new List<Ability>();
    internal List<Effect> effects = new List<Effect>();

    public abstract void InitAbilities();

    // TODO: move somewhere
    //void CreateFloatingText(Transform target, int value)
    //{
    //    var floatingText = Object.Instantiate(UIManager.instance.floatingTextPrefab, target);
    //    var textObject = floatingText.GetComponent<TextMeshProUGUI>();

    //    if (value > 0)
    //    {
    //        textObject.text = $"+{value}";
    //        textObject.color = Color.green;
    //    }

    //    if (value < 0)
    //    {
    //        textObject.text = $"{value}";
    //        textObject.color = Color.red;
    //    }
    //}
}