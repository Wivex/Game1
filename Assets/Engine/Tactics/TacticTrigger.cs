using System;

public enum TriggerType
{
    Any,
    EnemyType,
    StatusEffectType,
    StatValue,
    CanUseAbility,
    HasConsumable
}

public enum TargetType
{
    Hero,
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
    [PopupValue("abilitiesNames", order = 1), HideIfNotEnumValues("triggerType", TriggerType.CanUseAbility)]
    public string abilityName;
    [HideIfNotEnumValues("triggerType", TriggerType.EnemyType)]
    public UnitData unitData;

    public bool Triggered(Combat combat)
    {
        switch (triggerType)
        {
            case TriggerType.Any:
                return true;
            //case TriggerType.StatValue:
            //    return StatValueCheck(situation);
            case TriggerType.EnemyType:
                return combat.actor is Hero
                    ? combat.enemy.data.name == unitData.name
                    : combat.hero.data.classType.ToString() == unitData.name;
            case TriggerType.CanUseAbility:
                var abil = combat.actor.abilities.Find(ab => ab.data.name == abilityName);
                return abil.Ready(combat.actor);
            default:
                throw new ArgumentException();
        }
    }
}