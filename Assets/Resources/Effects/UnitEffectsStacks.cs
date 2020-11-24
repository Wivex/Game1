using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

internal class EffectsStack
{
    internal EffectOverTimeType type;
    internal List<EffectOverTime> effects;

    internal int cumulitiveDirectAmount;
    internal List<StatMod> cumulitiveStatMods;

    internal EffectsStack(EffectOverTime effect)
    {
        type = effect.data.type;
        effects = new List<EffectOverTime> {effect};
    }

    internal void CalculateCumulativeEffect()
    {
        cumulitiveDirectAmount = 0;
        cumulitiveStatMods = new List<StatMod>(type.statMods);
        cumulitiveStatMods.ForEach(stat => stat.value = 0);

        foreach (var effect in effects)
        {
            if (effect.curDelay <= 0)
            {
                // sum all direct effect amounts
                cumulitiveDirectAmount += effect.data.type.amount;
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

internal class UnitEffectsStacks
{
    internal Dictionary<EffectOverTimeType, EffectsStack> effectStacks =
        new Dictionary<EffectOverTimeType, EffectsStack>();
    internal int unappliedStacks;

    internal void CalculateEffectStacksPower()
    {
        unappliedStacks = effectStacks.Count;
        effectStacks.ForEach(stack => stack.Value.CalculateCumulativeEffect());
    }

    internal void Add(EffectOverTime effect)
    {
        if (effectStacks.ContainsKey(effect.data.type))
            effectStacks[effect.data.type].effects.Add(effect);
        else
            effectStacks.Add(effect.data.type, new EffectsStack(effect));
    }

    internal void Remove(EffectOverTime effect)
    {
        effectStacks[effect.data.type].effects.Remove(effect);
        effectStacks.Add(effect.data.type, new EffectsStack(effect));
    }

    internal EffectOverTimeType ApplyNextEffectStack(Unit unit)
    {
        var effectType = effectStacks.ElementAt(unappliedStacks).Key;
        switch (effectType.name)
        {
            case "Damage":
                unit.ApplyDamage(new Damage(effectType.damageType, effectStacks[effectType].cumulitiveDirectAmount));
                break;
            default:
                Debug.Log($"{effectType.name} is not yet implemented");
                break;
        }

        for (var i = 0; i < effectStacks[effectType].effects.Count; i++)
        {
            var effect = effectStacks[effectType].effects[i];
            effect.curDelay--;
            effect.curDuration--;

            if (effect.curDuration <= 0)
            {
                Remove(effect);
                // adjust i to check same index next iteration
                i--;
            }
        }

        unappliedStacks--;

        return effectType;
    }
}