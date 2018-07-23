using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Ability
{
    public AbilityData abilityData;
    public int curCooldown;

    public Ability(AbilityData abilityData)
    {
        this.abilityData = abilityData;
    }
}