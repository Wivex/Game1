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
    //public override int BaseValue
    //{
    //    get { return baseValue; }
    //    set
    //    {
    //        // adjust current value
    //        curValue = Math.Max(curValue + (value - baseValue), 1);
    //        baseValue = value;
    //        Recalculate();
    //    }
    //}

    //protected override void Recalculate()
    //{
    //    lastMaxValue = maxValue;
    //    maxValue = baseValue;
    //    var sumPercentAdd = 0;
    //    for (var i = 0; i < modifiers.Count; i++)
    //    {
    //        var mod = modifiers[i];
    //        switch (mod.modifierType)
    //        {
    //            case StatModType.Flat:
    //                maxValue += mod.amount;
    //                break;
    //            case StatModType.PercentAdd:
    //                //start adding together all modifiers of this type
    //                sumPercentAdd += mod.amount;
    //                //if we're at the end of the list OR the next modifer isn't of this type (all are sorted)
    //                if (i + 1 >= modifiers.Count || modifiers[i + 1].modifierType != StatModType.PercentAdd)
    //                {
    //                    //NOTE: optimize type changes?
    //                    //stop summing the additive multiplier
    //                    maxValue = (int)(maxValue * (1 + (float)sumPercentAdd/100));
    //                    sumPercentAdd = 0;
    //                }
    //                break;
    //            case StatModType.PercentMult:
    //                maxValue *= 1 + mod.amount;
    //                break;
    //            default:
    //                throw new ArgumentException();
    //        }

    //        // adjust current value
    //        curValue += maxValue - lastMaxValue;
    //    }
    //}

    #region OPERATORS

    //public static bool operator <(Stat a, Stat b) => a.curValue < b.curValue;

    //public static bool operator <=(Stat a, Stat b) => a.curValue <= b.curValue;

    //public static bool operator >(Stat a, Stat b) => a.curValue > b.curValue;

    //public static bool operator >=(Stat a, Stat b) => a.curValue >= b.curValue;

    //public static int operator *(Stat a, Stat b) => a.curValue * b.curValue;

    //public static int operator *(Stat a, int b) => a.curValue * b;

    //public static int operator *(Stat a, double b) => (int)(a.curValue * b);

    //public static int operator +(Stat a, Stat b) => a.curValue + b.curValue;

    //public static int operator +(Stat a, int b) => a.curValue + b;

    //public static int operator +(Stat a, double b) => (int)(a.curValue + b);

    //public static int operator -(Stat a, Stat b) => a.curValue - b.curValue;

    //public static int operator -(Stat a, int b) => a.curValue - b;

    //public static int operator -(Stat a, double b) => (int)(a.curValue - b);

    #endregion
}