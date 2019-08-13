using System;

public enum TriggerType
{
    Any,
    StatValue,
    FoeType,
    SelfCondition,
    FoeCondition,
    AbilityReady
}

public enum Target
{
    Self,
    Foe
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

    //[ShownIfEnumValue("triggerType", (int) TriggerType.StatValue, (int)TriggerType.AbilityReady)]
    public Target target;

    //[ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public StatType stat;

    //[ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public StatTriggerType statTriggerType;

    //[ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public ComparisonType comparisonType;

    //[ShownIfEnumValue("triggerType", (int) TriggerType.StatValue)]
    public int statValue;

    //[ShownIfEnumValue("triggerType", (int) TriggerType.AbilityReady)]
    public AbilityData abilityData;

    //[ShownIfEnumValue("triggerType", (int) TriggerType.FoeType)]
    public UnitData unitData;

    public bool IsTriggered(CombatManager situation)
    {
        switch (triggerType)
        {
            case TriggerType.Any:
                return true;
            //case TriggerType.StatValue:
            //    return StatValueCheck(situation);
            case TriggerType.FoeType:
                return situation.enemy.enemyData == unitData;
            case TriggerType.SelfCondition:
                throw new NotImplementedException();
            case TriggerType.FoeCondition:
                throw new NotImplementedException();
            case TriggerType.AbilityReady:
                return situation.actor.abilities.Exists(ability =>
                    ability.abilityData == abilityData && ability.curCooldown <= 0);
            default:
                throw new ArgumentException();
        }
    }

    #region CHECKS
    //public bool StatValueCheck(SituationCombat situation)
    //{
    //    //var unit = target == Target.Self ? situation.actor : situation.target;
    //    //return comparisonType == ComparisonType.LessOrEqual
    //    //    ? unit.baseStats[(int)stat].curValue <= statValue
    //    //    : unit.baseStats[(int)stat].curValue > statValue;
    //}

    public bool AbilityReadyCheck(CombatManager situation)
    {
        var unit = target == Target.Self ? situation.actor : situation.target;
        return unit.abilities.Exists(ability =>
            ability.abilityData == abilityData && ability.curCooldown == 0);
    }
    #endregion
}