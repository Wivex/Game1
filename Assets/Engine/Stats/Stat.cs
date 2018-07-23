using System;
using System.Collections.Generic;
using UnityEngine;

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
    public int curValue;
    [SerializeField]
    protected int baseValue;
    public List<StatModifier> modifiers = new List<StatModifier>();

    public virtual int BaseValue
    {
        get { return baseValue; }
        set
        {
            baseValue = value;
            Recalculate();
        }
    }

    /// <summary>
    /// reapply all mods to base value
    /// </summary>
    protected virtual void Recalculate()
    {
        curValue = baseValue;
        var sumPercentAdd = 0;
        for (var i = 0; i < modifiers.Count; i++)
        {
            var mod = modifiers[i];
            switch (mod.modifierType)
            {
                case StatModType.Flat:
                    curValue += mod.amount;
                    break;
                case StatModType.PercentAdd:
                    //start adding together all modifiers of this type
                    sumPercentAdd += mod.amount;
                    //if we're at the end of the list OR the next modifer isn't of this type (all are sorted)
                    if (i + 1 >= modifiers.Count || modifiers[i + 1].modifierType != StatModType.PercentAdd)
                    {
                        //NOTE: optimize type changes?
                        //stop summing the additive multiplier
                        curValue = (int)(curValue * (1 + (float)sumPercentAdd/100));
                        sumPercentAdd = 0;
                    }
                    break;
                case StatModType.PercentMult:
                    curValue *= 1 + mod.amount;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }

    public void AddModifier(StatModifier mod)
    {
        modifiers.Add(mod);
        modifiers.Sort(StatModifier.ModifierOrderComparison);
        Recalculate();
    }

    public void RemoveModifier(StatModifier mod)
    {
        modifiers.Remove(mod);
        Recalculate();
    }

    public void RemoveAllModsFromSource(object source)
    {
        if (modifiers.RemoveAll(mod => mod.source == source) > 0)
            Recalculate();
    }

    #region OPERATORS
    public static bool operator <(Stat a, Stat b) => a.curValue < b.curValue;

    public static bool operator <=(Stat a, Stat b) => a.curValue <= b.curValue;

    public static bool operator >(Stat a, Stat b) => a.curValue > b.curValue;

    public static bool operator >=(Stat a, Stat b) => a.curValue >= b.curValue;

    public static int operator *(Stat a, Stat b) => a.curValue * b.curValue;

    public static int operator *(Stat a, int b) => a.curValue * b;

    public static int operator *(Stat a, double b) => (int)(a.curValue * b);

    public static int operator +(Stat a, Stat b) => a.curValue + b.curValue;

    public static int operator +(Stat a, int b) => a.curValue + b;

    public static int operator +(Stat a, double b) => (int)(a.curValue + b);

    public static int operator -(Stat a, Stat b) => a.curValue - b.curValue;

    public static int operator -(Stat a, int b) => a.curValue - b;

    public static int operator -(Stat a, double b) => (int)(a.curValue - b);

    #endregion
}