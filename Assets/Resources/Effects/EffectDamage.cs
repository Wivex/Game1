using System;
using UnityEngine;

public class EffectDamage : Effect
{
    protected override void ProcEffect()
    {
        targetUnit.TakeDamage(combat.exp, new Damage(damageType, amount));

        base.ProcEffect();
    }
}