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
        curCooldown = 0;
    }

    public void Use(Unit actor, Unit target)
    {
        if (abilityData.damage != 0)
            target.TakeDamage(new Damage(abilityData.damageType, abilityData.damage));
        //TryApplyEffects(actor, target);
        CDReset();
    }

    public void CDReset()
    {
        curCooldown = abilityData.cooldown;
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