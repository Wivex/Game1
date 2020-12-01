using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

internal class EffectStackElement
{
    internal int delay, duration, value;

    internal EffectStackElement(EffectOverTime effect)
    {
        delay = effect.data.delay;
        duration = effect.data.duration;
        value = effect.data.value;
    }
}

internal class EffectOverTime
{
    internal EffectOverTimeData data;
    internal int curDuration, curDelay, curValue;
    internal List<EffectStackElement> stack = new List<EffectStackElement>();

    internal EffectOverTime(EffectOverTimeData data)
    {
        this.data = data;
    }
        
    internal void AddStack(EffectOverTime effect)
    {
        stack.Add(new EffectStackElement(effect));
    }

    internal void UpdateStack(EffectOverTime effect)
    {
        if (stack.NotNullOrEmpty())
        {
            // recalculate directEffect and statMod values
            foreach (var effect in elements)
            {
                if (effect.curDelay <= 0)
                {
                    // sum all direct effect amounts
                    cumulativeEffect.value += effect.data.value;
                    foreach (var statMod in effect.data.type.statMods)
                    {
                    
                    }
                    for (var i = 0; i < type.statMods.Count; i++)
                    {
                        var statMod = effect.data.type.statMods[i];
                        if (statMod.stacks)
                            cumulitiveStatMods[i].value += statMod.value;
                        else
                            cumulitiveStatMods[i].value = statMod.value;
                    }
                }
            }
        }
    }

    internal void RecalculateStackEffect()
    {
        // reset cumulativeEffect
        cumulativeEffect.value = 0;
        cumulativeEffect = new List<StatMod>(cumulativeEffect.type.statMods);
        cumulativeEffect.ForEach(stat => stat.value = 0);

        // recalculate directEffect and statMod values
        foreach (var effect in elements)
        {
            if (effect.curDelay <= 0)
            {
                // sum all direct effect amounts
                cumulativeEffect.value += effect.data.value;
                foreach (var statMod in effect.data.type.statMods)
                {
                    
                }
                for (var i = 0; i < type.statMods.Count; i++)
                {
                    var statMod = effect.data.type.statMods[i];
                    if (statMod.stacks)
                        cumulitiveStatMods[i].value += statMod.value;
                    else
                        cumulitiveStatMods[i].value = statMod.value;
                }
            }
        }
    }
}