using System;
using UnityEngine;

public class EffectHealing : Effect
{
    protected override void ProcEffect()
    {
        targetUnit.Heal(amount);

        base.ProcEffect();
    }
}