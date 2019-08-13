using System;
using System.Linq;

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

    //[ShownIfEnumValue("actionType", (int) ActionType.UseAbility)]
    public AbilityData abilityData;
    //[ShownIfEnumValue("actionType", (int)ActionType.UseConsumable)]
    public ConsumableData consumableData;

    public void DoAction(CombatManager situation)
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
                UseConsumable(situation);
                break;
        }
    }

    #region ACTIONS

    public void Flee(CombatManager situation)
    {
        //LogEvent(situation, $"{situation.actor.name} flees from combat.");
    }

    public void Attack(CombatManager situation)
    {
        //var damage = new Damage(DamageType.Physical,
        //    situation.actor.baseStats[(int) StatType.Attack].curValue);
        //var dam = situation.target.TakeDamage(damage);

        ////situation.expedition.expPreviewPanel.redrawFlags.health = true;

        //situation.expedition.UpdateLog(
        //    $"{situation.actor.name} attacks {situation.target.name} for {dam} {damage.type} damage.");
    }

    public void UseAbility(CombatManager situation)
    {
        //LogEvent(situation, $"{situation.actor.name} used {abilityData.name} on {situation.target.name}.");
        var usedAbility = situation.actor.abilities.Find(abil => abil.abilityData == abilityData);
        foreach (var effect in usedAbility.abilityData.effects)
        {
            var target = effect.target == Target.Self ? situation.actor : situation.target;
            effect.ApplyEffect(situation, target, usedAbility.abilityData.name, usedAbility.abilityData.icon);
        }
        // +1 adjustment, because after each turm all cooldowns are decreased by 1 (even on used ability)
        usedAbility.curCooldown = abilityData.cooldown + 1;
    }

    public void UseConsumable(CombatManager situation)
    {
        //LogEvent(situation, $"{situation.hero.name} used {consumableData.name} on {situation.target.name}.");
        var usedConsumable = situation.hero.consumables.First(cons => cons.consumableData == consumableData);
        foreach (var effect in usedConsumable.consumableData.effects)
        {
            var target = effect.target == Target.Self ? situation.actor : situation.target;
            effect.ApplyEffect(situation, target, usedConsumable.consumableData.name, usedConsumable.consumableData.icon);
        }
        // +1 adjustment, because after each turm all cooldowns are decreased by 1 (even on used ability)
        usedConsumable.curCharges--;
    }

    public void LogEvent(CombatManager situation, string text)
    {
        situation.expedition.UpdateLog(text);
    }
    #endregion
}