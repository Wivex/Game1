using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Flee,
    Attack,
    UseAbility,
    UseConsumable
}

[Serializable]
public class TacticAction
{
    public ActionType actionType;

    [ShownIfEnumValue("actionType", (int)ActionType.UseAbility)]
    public AbilityData abilityData;
}