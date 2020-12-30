using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

internal class EffectStackElement
{
    internal int delay, duration, value;

    internal EffectStackElement(EffectOverTime effect)
    {
        delay = effect.type.delay;
        duration = effect.type.duration;
        value = effect.type.value;
    }
}

internal class EffectOverTime
{
    internal EffectOverTimeType type;
    internal int duration, delay, value;

    List<EffectStackElement> stack = new List<EffectStackElement>();

    internal EffectOverTime(EffectOverTimeData data)
    {
        type = data.type;
    }
        
    internal void AddStackElement(EffectOverTime effect)
    {
        stack.Add(new EffectStackElement(effect));
        UpdateStack();
    }

    internal void UpdateStack()
    {
        value = 0;
        // recalculate directEffect and statMod values
        foreach (var stackElem in stack)
        {
            if (stackElem.delay <= 0)
            {
                // sum all direct effect amounts
                value += stackElem.value;
                foreach (var statMod in stackElem.type.statMods)
                {
                    
                }
                for (var i = 0; i < type.statMods.Count; i++)
                {
                    var statMod = type.type.statMods[i];
                    if (statMod.stacks)
                        cumulitiveStatMods[i].value += statMod.value;
                    else
                        cumulitiveStatMods[i].value = statMod.value;
                }
            }
        }
    }
}