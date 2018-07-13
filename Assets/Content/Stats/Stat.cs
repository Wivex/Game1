using System;
using System.Collections.Generic;

public enum StatType
{
    Health,
    Mana,
    Attack,
    Defence,
    Speed,
    HResist,
    BResist
}

[Serializable]
public class Stat
{
    public int maxValue, curValue;
    public List<StatModifier> modifiers = new List<StatModifier>();

    public int MaxValue
    {
        get { return maxValue; }
        set
        {
            maxValue = value;
            ApplyModifiers(maxValue);
        }
    }

    public int CurValue
    {
        get { return curValue; }
        set
        {
            curValue = value;
            ApplyModifiers(curValue);
        }
    }

    private void ApplyModifiers(int baseValue)
    {
        var newValue = baseValue;

        var sumPercentAdd = 0;
        for (var i = 0; i < modifiers.Count; i++)
        {
            var mod = modifiers[i];
            switch (mod.type)
            {
                case StatModType.Flat:
                    newValue += mod.value;
                    break;
                case StatModType.PercentAdd:
                    //start adding together all modifiers of this type
                    sumPercentAdd += mod.value;
                    //if we're at the end of the list OR the next modifer isn't of this type (all are sorted)
                    if (i + 1 >= modifiers.Count || modifiers[i + 1].type != StatModType.PercentAdd)
                    {
                        //NOTE: optimize type changes?
                        //stop summing the additive multiplier
                        newValue = (int)(newValue * (1 + (float)sumPercentAdd/100));
                        sumPercentAdd = 0;
                    }
                    break;
                case StatModType.PercentMult:
                    newValue *= 1 + mod.value;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public void AddModifier(StatModifier mod)
    {
        modifiers.Add(mod);
        modifiers.Sort(StatModifier.ModifierOrderComparison);
    }

    public void RemoveModifier(StatModifier mod)
    {
        modifiers.Remove(mod);
        ApplyModifiers(maxValue);
    }

    public void RemoveAllModsFromSource(object source)
    {
        if (modifiers.RemoveAll(mod => mod.source == source) > 0)
            ApplyModifiers(maxValue);
    }
}