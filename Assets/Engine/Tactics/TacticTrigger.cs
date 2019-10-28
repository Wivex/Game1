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
    Opponent
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
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue, TriggerType.AbilityReady)]
    public Target target;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public StatType stat;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public StatTriggerType statTriggerType;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public ComparisonType comparisonType;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public int statValue;
    [HideIfNotEnumValues("triggerType", TriggerType.AbilityReady)]
    public AbilityData abilityData;
    [HideIfNotEnumValues("triggerType", TriggerType.FoeType)]
    public UnitData unitData;

    public bool IsTriggered(Enemy enemy)
    {
        switch (triggerType)
        {
            case TriggerType.Any:
                return true;
            //case TriggerType.StatValue:
            //    return StatValueCheck(situation);
            case TriggerType.FoeType:
                // NOTE: rework?
                return enemy.data == unitData;
            case TriggerType.SelfCondition:
                throw new NotImplementedException();
            case TriggerType.FoeCondition:
                throw new NotImplementedException();
            case TriggerType.AbilityReady:
                //return situation.actor.abilities.Exists(ability =>
                //    ability.abilityData == abilityData && ability.curCooldown <= 0);
                return true;
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
        //var unit = target == Target.Self ? situation.actor : situation.target;
        //return unit.abilities.Exists(ability =>
        //    ability.abilityData == abilityData && ability.curCooldown == 0);
        return true;
    }
    #endregion
}