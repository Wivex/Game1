using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    Any,
    StatValue,
    EnemyType,
    HeroCondition,
    EnemyCondition,
    AbilityReady,
    MinTurnDelay
}

public enum Target
{
    Hero,
    Enemy
}

public enum ComparisonType
{
    LessOrEqual,
    More
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

    [ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public Target target;

    [ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public StatType stat;

    [ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public StatTriggerType statTriggerType;

    [ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public ComparisonType comparisonType;

    [ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public int statValue;

    [ShownIfEnumValue("triggerType", (int) TriggerType.AbilityReady)]
    public AbilityData abilityData;

    [ShownIfEnumValue("triggerType", (int) TriggerType.EnemyType)]
    public EnemyData enemyData;

    [ShownIfEnumValue("triggerType", (int) TriggerType.MinTurnDelay)]
    public int minTurn;

    public bool IsTriggered(Hero hero, Enemy enemy, Unit actor)
    {
        switch (triggerType)
        {
            case TriggerType.Any:
                return true;
            case TriggerType.StatValue:
                switch (target)
                {
                    case Target.Hero:
                        return StatsCheck(hero);
                    case Target.Enemy:
                        return StatsCheck(enemy);
                }
                break;
            case TriggerType.EnemyType:
                return enemy.enemyData == enemyData;
            case TriggerType.HeroCondition:
                throw new NotImplementedException();
            case TriggerType.EnemyCondition:
                throw new NotImplementedException();
            case TriggerType.AbilityReady:
                return actor.abilities.Exists(ability =>
                    ability.abilityData == abilityData && ability.curCooldown == 0);
            case TriggerType.MinTurnDelay:
                throw new NotImplementedException();
            default:
                throw new ArgumentOutOfRangeException();
        }
        throw new ArgumentOutOfRangeException();
    }

    #region CHECKS
    public bool StatsCheck(Unit unit)
    {
        switch (comparisonType)
        {
            case ComparisonType.LessOrEqual:
                if (unit.stats[(int) stat].curValue <= statValue)
                    return true;
                break;
            case ComparisonType.More:
                if (unit.stats[(int) stat].curValue > statValue)
                    return true;
                break;
        }

        throw new ArgumentOutOfRangeException();
    }
    #endregion
}