public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult
}

public class StatModifier
{
    // readonly to make parameters be set only by constructor
    public readonly int value;
    public readonly StatModType type;
    //object can hold any possible source type
    public readonly object source;
    public readonly int order;

    public StatModifier(int value, StatModType type, object source, int order)
    {
        this.value = value;
        this.type = type;
        this.source = source;
        this.order = order;
    }

    public StatModifier(int value, StatModType type) : this(value, type, null, (int)type)
    {
    }

    public StatModifier(int value, StatModType type, object source) : this(value, type, source, (int)type)
    {
    }

    public static int ModifierOrderComparison(StatModifier a, StatModifier b)
    {
        return a.order < b.order ? -1 : (a.order > b.order ? 1 : 0);
    }
}