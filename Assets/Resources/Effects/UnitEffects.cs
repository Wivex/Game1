using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

internal class UnitEffects
{
    internal int unappliedEffectsNumber;

    Dictionary<EffectOverTimeType, EffectOverTime> activeEffects = new Dictionary<EffectOverTimeType, EffectOverTime>();

    internal void Add(EffectOverTime effect)
    {
        var eType = effect.data.type;
        if (activeEffects.ContainsKey(eType))
        {
            activeEffects[eType].AddStack(effect);
        }
        else
            activeEffects.Add(eType, effect);
    }

    internal void CalculateEffectPower()
    {
        unappliedEffectsNumber = effectTypes.Count;
        effectTypes.ForEach(stack => stack.Value.CalculateCumulativeEffect());
    }

    internal void Remove(EffectOverTime effect)
    {
        effectTypes[effect.data.type].stack.Remove(effect);
    }

    internal EffectOverTimeType ApplyNextEffectType(Unit unit)
    {
        var effectType = effectTypes.ElementAt(unappliedEffectsNumber - 1).Key;
        switch (effectType.directEffect)
        {
            case EffectDirectType.Damage:
                unit.ApplyDamage(new Damage(effectType.damageType, effectTypes[effectType].cumulitiveDirectAmount));
                break;
            default:
                Debug.Log($"{effectType.directEffect} is not yet implemented");
                break;
        }

        for (var i = 0; i < effectTypes[effectType].stack.Count; i++)
        {
            var effect = effectTypes[effectType].stack[i];
            effect.curDelay--;
            effect.curDuration--;

            if (effect.curDuration <= 0)
            {
                Remove(effect);
                // adjust i to check same index next iteration
                i--;
            }
        }

        unappliedEffectsNumber--;

        return effectType;
    }
}