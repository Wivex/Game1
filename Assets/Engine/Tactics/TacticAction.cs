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

    [ShownIfEnumValue("actionType", (int) ActionType.UseAbility)]
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
            situation.actor.stats[(int) StatType.Attack].curValue);
        var dam = situation.target.TakeDamage(damage);

        situation.expedition.UpdateLog(
            $"{situation.actor.name} attacks {situation.target.name} for {dam} {damage.type} damage.");
    }

    public void UseAbility(SituationCombat situation)
    {
        LogEvent(situation, $"{situation.actor.name} used {abilityData.name} on {situation.target.name}.");
        var usedAbility = situation.actor.abilities.Find(abil => abil.abilityData == abilityData);
        foreach (var effect in usedAbility.abilityData.effects)
        {
            var target = effect.target == Target.Self ? situation.actor : situation.target;
            effect.ApplyEffect(situation, target, usedAbility.abilityData);
        }
        // +1 adjustment, because after each turm all cooldowns are decreased by 1 (even on used ability)
        usedAbility.curCooldown = abilityData.cooldown + 1;
    }

    public void LogEvent(SituationCombat situation, string text)
    {
        situation.expedition.UpdateLog(text);
    }
    #endregion
}