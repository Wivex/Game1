using System;
using System.Collections;
using System.Collections.Generic;
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
                break;
            case ActionType.UseConsumable:
                throw new NotImplementedException();
        }
        throw new ArgumentOutOfRangeException();
    }

    #region MyRegion
    public void Attack(SituationCombat situation)
    {
        var damage = new Damage(DamageType.Physical,
            situation.actor.stats[(int)StatType.Attack] - situation.target.stats[(int)StatType.Defence]);

        situation.expedition.expeditionPanel.UpdateLog(
            $"{situation.actor.name} attacks {situation.target.name} for {damage.amount} {damage.type} damage.");
        situation.target.TakeDamage(damage);
    }
    #endregion
}