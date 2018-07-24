using System;

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
                Flee(situation);
                break;
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
    public void Flee(SituationCombat situation)
    {
        LogEvent(situation, $"{situation.actor.name} flees from combat.");
    }

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
        foreach (var effect in usedAbility.abilityData.effects) effect.ApplyEffect(situation);
        usedAbility.curCooldown = abilityData.cooldown;
    }

    public void LogEvent(SituationCombat situation, string text)
    {
        situation.expedition.expeditionPanel.UpdateLog(text);
    }
    #endregion
}