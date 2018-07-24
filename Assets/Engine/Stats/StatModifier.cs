using System;

public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult
}

//NOTE: add stat type reference?
[Serializable]
public class StatModifier
{
    // readonly to make parameters be set only by constructor
    public StatType stat;
    public StatModType modifierType;
    public int amount;
    //object can hold any possible source type
    public object source;
    private int order;

    public StatModifier(int amount, StatModType modifierType, object source, int order)
    {
        this.amount = amount;
        this.modifierType = modifierType;
        this.source = source;
        this.order = order;
    }

    public StatModifier(int amount, StatModType modifierType, object source) : this(amount, modifierType, source, (int)modifierType)
    {
    }

    public static int ModifierOrderComparison(StatModifier a, StatModifier b)
    {
        return a.order < b.order ? -1 : (a.order > b.order ? 1 : 0);
    }
}