using System;
using UnityEngine;

public class ChangeResourceStat : Effect
{
    internal override void ProcEffect()
    {
        swi
        targetUnit.TakeDamage(combat.exp, new Damage(damageType, amount));

        base.ProcEffect();
    }
}