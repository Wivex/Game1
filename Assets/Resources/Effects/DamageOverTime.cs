using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

internal class DamageOverTime
{
    internal EffectOverTimeData data;
    internal int curDuration, curDelay, curValue;

    List<EffectStackElement> stack = new List<EffectStackElement>();

    internal EffectOverTime(EffectOverTimeData data)
    {
        this.data = data;
    }
        
    internal void AddStack(EffectOverTime effect)
    {
        stack.Add(new EffectStackElement(effect));
        RecalculateCurrentEffect();
    }

    internal void RemoveStack(EffectOverTime effect)
    {
        //stack.Add(new EffectStackElement(effect));
        //RecalculateCumulativeEffect();
    }

    internal void Update()
    {
    }

    internal void RecalculateCurrentEffect()
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