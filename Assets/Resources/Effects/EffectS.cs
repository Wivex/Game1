using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class EffectS
{
    internal EffectOverTimeData cumulativeEffect;
    List<EffectOverTime> elements;

    public EffectS(EffectOverTime effect)
    {
        elements = new List<EffectOverTime>();
        cumulativeEffect = new EffectOverTimeData();
        {

        }
        cumulativeEffect
    }
    
    internal void Add(EffectOverTime effect)
    {
        elements.Add(effect);
        RecalculateCumulativeEffect();
    }

    internal void RecalculateCumulativeEffect()
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