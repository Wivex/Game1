using System;
using UnityEngine;

public class Ability
{
    internal AbilityData data;
    internal int curCooldown;

    internal Ability(AbilityData data)
    {
        this.data = data;
    }

    internal void ApplyDirectEffects(Combat combat)
    {
        foreach (var effect in data.effectsDirect)
        {
            var targetUnit = effect.target == TargetType.Hero ? (Unit) combat.hero : combat.enemy;
            switch (effect.directEffect)
            {
                case EffectDirectType.Damage:
                    combat.mis.ApplyDamage(targetUnit, new Damage(effect.damageType, effect.amount));
                    break;
                case EffectDirectType.Heal:
                    Debug.Log($"{effect.directEffect} is not yet implemented");
                    break;
                case EffectDirectType.EnergyGain:
                    Debug.Log($"{effect.directEffect} is not yet implemented");
                    break;
                case EffectDirectType.EnergyLoss:
                    Debug.Log($"{effect.directEffect} is not yet implemented");
                    break;
            }
        }
    }

    internal bool Ready(Unit unit) =>
        curCooldown <= 0 && unit.Energy >= data.energyCost;

    internal void UpdateCooldown() => curCooldown = Math.Max(curCooldown--, 0);
}