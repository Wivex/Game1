using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Ability
{
    public AbilityData data;
    public int curCooldown;

    internal bool IsReady(Unit unit)
    {
        return curCooldown <= 0 &&  && unit.Energy >= data.energyCost;
    }

    public Ability(AbilityData data)
    {
        this.data = data;
    }
}