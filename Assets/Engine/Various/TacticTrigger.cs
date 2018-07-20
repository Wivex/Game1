using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    Any,
    StatValue,
    EnemyType,
    SelfCondition,
    FoeCondition,
    AbilityReady,
    MinTurnDelay
}

public enum StatTriggerType
{
    Value,
    Percent
}

[Serializable]
public class TacticTrigger
{
    public TriggerType triggerType;

    [ShownIfEnumValue("triggerType", (int)TriggerType.StatValue)]
    public StatType stat;
    [ShownIfEnumValue("triggerType", (int)TriggerType.StatValue)]
    public StatTriggerType statTriggerType;
    [ShownIfEnumValue("triggerType", (int)TriggerType.StatValue)]
    public int statValue;

    [ShownIfEnumValue("triggerType", (int)TriggerType.AbilityReady)]
    public AbilityData abilityData;

    [ShownIfEnumValue("triggerType", (int)TriggerType.EnemyType)]
    public EnemyData enemyData;

    [ShownIfEnumValue("triggerType", (int)TriggerType.MinTurnDelay)]
    public int minTurn;
}