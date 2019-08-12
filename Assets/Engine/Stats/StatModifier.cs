using System;

public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult
}

[Serializable]
public class StatModifier
{
    public StatType stat;
    public StatModType modType;
    public int value;
    //object can hold any possible source type
    public object source;
    public int order;

    public StatModifier(int value, StatModType modType, object source, int order)
    {
        this.value = value;
        this.modType = modType;
        this.source = source;
        this.order = order;
    }

    public StatModifier(int value, StatModType modType, object source) : this(value, modType, source, (int)modType)
    {
    }

    public static int ModifierOrderComparison(StatModifier a, StatModifier b)
    {
        return a.order < b.order ? -1 : (a.order > b.order ? 1 : 0);
    }
}