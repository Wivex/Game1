using System;

public enum TriggerType
{
    Any,
    EnemyType,
    StatusEffectType,
    StatValue,
    AbilityReady,
    HasConsumable
}

public enum TargetType
{
    Self,
    Enemy
}

public enum ComparisonType
{
    Less,
    LessOrEqual,
    Equal,
    NotEqual,
    MoreOrEqual,
    More
}

public enum MeasureType
{
    Value,
    Percent
}

public enum StatusEffectType
{
    Normal,
    Burning
}

[Serializable]
public class TacticTrigger
{
    public TriggerType triggerType = TriggerType.Any;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue, TriggerType.StatusEffectType)]
    public TargetType targetType;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public StatType stat;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public MeasureType measureType;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public ComparisonType comparisonType;
    [HideIfNotEnumValues("triggerType", TriggerType.StatValue)]
    public int amount;
    [HideIfNotEnumValues("triggerType", TriggerType.AbilityReady)]
    public AbilityData abilityData;
    [HideIfNotEnumValues("triggerType", TriggerType.EnemyType)]
    public UnitData unitData;

    public bool IsTriggered(Enemy enemy)
    {
        switch (triggerType)
        {
            case TriggerType.Any:
                return true;
            //case TriggerType.StatValue:
            //    return StatValueCheck(situation);
            case TriggerType.EnemyType:
                // NOTE: rework?
                return enemy.data == unitData;
            case TriggerType.AbilityReady:
                //return situation.curActor.abilities.Exists(ability =>
                //    ability.abilityData == abilityData && ability.curCooldown <= 0);
                return true;
            default:
                throw new ArgumentException();
        }
    }

    #region TRIGGER TYPE CHECKS
    //public bool StatValueCheck(SituationCombat situation)
    //{
    //    //var unit = curTarget == Target.Self ? situation.curActor : situation.curTarget;
    //    //return comparisonType == ComparisonType.LessOrEqual
    //    //    ? unit.baseStats[(int)stat].curValue <= amount
    //    //    : unit.baseStats[(int)stat].curValue > amount;
    //}

    //public bool AbilityReadyCheck(CombatManager situation)
    //{
    //    //var unit = curTarget == Target.Self ? situation.curActor : situation.curTarget;
    //    //return unit.abilities.Exists(ability =>
    //    //    ability.abilityData == abilityData && ability.curCooldown == 0);
    //    return true;
    //}
    #endregion
}