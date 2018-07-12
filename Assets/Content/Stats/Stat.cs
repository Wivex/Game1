using System;
using System.Collections.Generic;

[Serializable]
public class Stat
{
    public int baseValue;
    public List<StatModifier> statModifiers;

    private bool BaseValueChanged => baseValue != lastBaseValue;
    private bool reqRecalculation;
    private int lastValue, lastBaseValue = int.MinValue;

    public float Value
    {
        get
        {
            if (BaseValueChanged)
            {
                lastBaseValue = baseValue;
                reqRecalculation = true;
            }

            if (reqRecalculation)
            {
                lastValue = RecalculateValue();
            }

            return lastValue;
        }
    }

    public Stat(int baseValue) :this()
    {
        this.baseValue = baseValue;
        //NOTE: needed?
        statModifiers = new List<StatModifier>();
    }

    //NOTE: needed?
    public Stat()
    {
        statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier mod)
    {
        statModifiers.Add(mod);
        statModifiers.Sort(StatModifier.ModifierOrderComparison);
        reqRecalculation = true;
    }

    public bool RemoveModifier(StatModifier mod)
    {
        return reqRecalculation = statModifiers.Remove(mod);
    }

    public bool RemoveAllModsFromSource(object source)
    {
        return reqRecalculation = statModifiers.RemoveAll(mod => mod.source == source) > 0;
    }

    private int RecalculateValue()
    {
        var newValue = baseValue;
        var sumPercentAdd = 0;

        for (var i = 0; i < statModifiers.Count; i++)
        {
            var curMod = statModifiers[i];
            switch (curMod.type)
            {
                case StatModType.Flat:
                    newValue += curMod.value;
                    break;
                case StatModType.PercentAdd:
                    //start adding together all modifiers of this type
                    sumPercentAdd += curMod.value;
                    //if we're at the end of the list OR the next modifer isn't of this type (all are sorted)
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModType.PercentAdd)
                    {
                        //NOTE: optimize type changes?
                        //stop summing the additive multiplier
                        newValue = (int)(newValue * (1 + (float)sumPercentAdd/100));
                        sumPercentAdd = 0;
                    }

                    break;
                case StatModType.PercentMult:
                    newValue *= 1 + curMod.value;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return newValue;
    }
}