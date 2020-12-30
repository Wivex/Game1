using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

internal class UnitEffects
{
    internal int unappliedEffectsCount;

    // Effect dictionary to for quick "by type" effect search
    List<EffectOverTime> effects = new List<EffectOverTime>();

    internal void Add(EffectOverTime newEffect)
    {
        var sameEffect = effects.Find(eff => eff.type == newEffect.type);
        if (sameEffect!=null)
        {
            sameEffect.AddStackElement(newEffect);
        }
        else
            effects.Add(newEffect);
    }

    internal void UpdateEffects()
    {
        unappliedEffectsCount = effects.Count;
        for (var i = 0; i < effects.Count; i++)
        {
            effects[i].UpdateStack();
            if (effects[i].duration <= 0)
            {
                effects.RemoveAt(i);
                i--;
            }
        }
    }

    internal EffectOverTimeType ApplyNextEffect(Unit unit)
    {
        switch (effects[unappliedEffectsCount-1].type.directEffect)
        {
            case EffectDirectType.Damage:
                unit.ApplyDamage(new Damage(eType.damageType, effectTypes[eType].cumulitiveDirectAmount));
                break;
            default:
                Debug.Log($"{eType.directEffect} is not yet implemented");
                break;
        }

        for (var i = 0; i < effectTypes[eType].stack.Count; i++)
        {
            var effect = effectTypes[eType].stack[i];
            effect.curDelay--;
            effect.curDuration--;

            if (effect.curDuration <= 0)
            {
                Remove(effect);
                // adjust i to check same index next iteration
                i--;
            }
        }

        unappliedEffectsCount--;

        return eType;
    }
}