using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionType
{
    Flee,
    Attack,
    UseAbility,
    UseConsumable
}

[Serializable]
public class TacticAction
{
    public ActionType actionType;
    [ShownIfEnumValue("actionType", (int)ActionType.UseAbility)]
    public AbilityData abilityData;

    public void DoAction(SituationCombat situation)
    {
        switch (actionType)
        {
            case ActionType.Flee:
                throw new NotImplementedException();
            case ActionType.Attack:
                Attack(situation);
                break;
            case ActionType.UseAbility:
                UseAbility(situation);
                break;
            case ActionType.UseConsumable:
                throw new NotImplementedException();
        }
    }

    #region ACTIONS
    public void Attack(SituationCombat situation)
    {
        var damage = new Damage(DamageType.Physical,
            situation.actor.stats[(int)StatType.Attack].curValue, situation.target);

        situation.expedition.expeditionPanel.UpdateLog(
            $"{situation.actor.name} attacks {situation.target.name} for {damage.amount} {damage.type} damage.");
        situation.target.TakeDamage(damage);
    }
    
    public void UseAbility(SituationCombat situation)
    {
        var usedAbility = situation.actor.abilities.Find(abil => abil.abilityData == abilityData);
        if (abilityData.damage != 0)
        {
            var damage = new Damage(abilityData.damageType,
                abilityData.damage, situation.target);

            situation.expedition.expeditionPanel.UpdateLog(
                $"{situation.actor.name} uses {abilityData.name} on {situation.target.name} for {damage.amount} {damage.type} damage.");
            situation.target.TakeDamage(damage);
        }
        usedAbility.curCooldown = abilityData.cooldown;
    }
    #endregion
}