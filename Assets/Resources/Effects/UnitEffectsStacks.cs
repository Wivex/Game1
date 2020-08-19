using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

internal class EffectsStack
{
    internal EffectOverTimeType type;
    internal List<EffectOverTime> effects = new List<EffectOverTime>();

    internal int cumulitiveDirectAmount;
    internal List<StatMod> cumulitiveStatMods;

    internal void CalculateCumulativeEffect()
    {
        cumulitiveDirectAmount = 0;
        cumulitiveStatMods = new List<StatMod>(type.statMods);
        cumulitiveStatMods.ForEach(stat => stat.value = 0);

        foreach (var effect in effects)
        {
            // effect not ready to be applied
            if (effect.curDelay > 0) effect.curDelay--;
            else
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

    internal void CalculateCumulativeUnitEffects()
    {
        unappliedStacks = effectStacks.Count;
        effectStacks.ForEach(stack => stack.Value.CalculateCumulativeEffect());
    }

    internal void ApplyNextCumulativeEffect(Mission mis, Unit unit)
    {
        if (unappliedStacks > 0)
        {
            var effectType = effectStacks.ElementAt(unappliedStacks).Key;
            switch (effectType.name)
            {
                case "Damage":
                    mis.ApplyDamage(unit, new Damage(effectType.damageType, effectStacks[effectType].cumulitiveDirectAmount));
                    break;
                default:
                    Debug.Log($"{effectType.name} is not yet implemented");
                    break;
            }

            unappliedStacks--;
        }
    }

    internal void RemoveEffect(Mission mis, Unit unit)
    {
        mis.RemoveEffect(unit, this);
    }
}