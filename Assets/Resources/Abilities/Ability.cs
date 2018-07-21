using System;
using TMPro;
using UnityEngine;

[Serializable]
public class Ability
{
    public AbilityData abilityData;
    public int curCooldown;

    public Ability(AbilityData abilityData)
    {
        this.abilityData = abilityData;
    }

    //public void TryApplyEffects(Unit actor, Unit target)
    //{
    //    foreach (var effect in effects)
    //    {
    //        //if (effect.XMLData.TargetSelf)
    //        //    actor.effects.Add(effect);
    //        //else
    //        //    target.effects.Add(effect);
    //    }
    //}
}