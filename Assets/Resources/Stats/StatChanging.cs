using System;

[Serializable]
public class StatChanging : Stat
{
    public int maxValue;
    private int lastMaxValue;

    public override int BaseValue
    {
        get { return baseValue; }
        set
        {
            // adjust current value
            curValue = Math.Max(curValue + (value - baseValue), 1);
            baseValue = value;
            Recalculate();
        }
    }

    protected override void Recalculate()
    {
        lastMaxValue = maxValue;
        maxValue = baseValue;
        var sumPercentAdd = 0;
        for (var i = 0; i < modifiers.Count; i++)
        {
            var mod = modifiers[i];
            switch (mod.modifierType)
            {
                case StatModType.Flat:
                    maxValue += mod.amount;
                    break;
                case StatModType.PercentAdd:
                    //start adding together all modifiers of this type
                    sumPercentAdd += mod.amount;
                    //if we're at the end of the list OR the next modifer isn't of this type (all are sorted)
                    if (i + 1 >= modifiers.Count || modifiers[i + 1].modifierType != StatModType.PercentAdd)
                    {
                        //NOTE: optimize type changes?
                        //stop summing the additive multiplier
                        maxValue = (int)(maxValue * (1 + (float)sumPercentAdd/100));
                        sumPercentAdd = 0;
                    }
                    break;
                case StatModType.PercentMult:
                    maxValue *= 1 + mod.amount;
                    break;
                default:
                    throw new ArgumentException();
            }

            // adjust current value
            curValue += maxValue - lastMaxValue;
        }
    }
}