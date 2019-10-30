using System;
using UnityEngine;

public class ChangeStatDepletable : Effect
{
    internal override void ProcEffect()
    {
        // UNDONE: add energy type switch
        switch (stat)
        {
            case StatType.Health:
                if (amount > 0)
                    targetUnit.Heal(amount);
                if (amount< 0)
                    targetUnit.TakeDamage(combat.exp, new Damage(damageType, amount));
                break;
            case StatType.Energy:
                switch (targetUnit)
                {
                        
                }

                break;
        }

        base.ProcEffect();
    }
}