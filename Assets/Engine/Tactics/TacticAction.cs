using System;
using System.Linq;

public enum ActionType
{
    Attack,
    UseAbility,
    UseConsumable,
    Flee
}

[Serializable]
public class TacticAction
{
    public ActionType actionType;

    [HideIfNotEnumValues("actionType", ActionType.UseAbility)]
    public AbilityData abilityData;

    [HideIfNotEnumValues("actionType", ActionType.UseConsumable)]
    public ConsumableData consumableData;

    public void DoAction(Combat combat)
    {
        switch (actionType)
        {
            case ActionType.Flee:
                Flee(combat);
                break;
            case ActionType.Attack:
                Attack(combat);
                break;
            case ActionType.UseAbility:
                UseAbility(combat);
                break;
            case ActionType.UseConsumable:
                UseConsumable(combat);
                break;
        }
    }


    #region ACTIONS

    public void Flee(Combat combat)
    {
        //LogEvent(combat, $"{combat.actor.name} flees from combat.");
    }

    public void Attack(Combat combat)
    {
        combat.exp.StartAnimation(AnimationTrigger.Attack, combat.GetAnimManager(combat.actor));

        var damTaken = combat.target.TakeDamage(combat.exp, new Damage(DamageType.Physical, combat.actor.curStats.attack));

        UIManager.i.CreateEffectAnimation(combat.exp, combat.target, UIManager.i.meleeHitPrefab);

        //combat.exp.UpdateLog($"{combat.actor} attacks {combat.target} for {dam} {damage.type} damage.");
    }

    public void UseAbility(Combat combat)
    {
        var usedAbility = combat.actor.abilities.Find(abil => abil.abilityData == abilityData);
        foreach (var effect in usedAbility.abilityData.effects)
        {
            var target = effect.target == Target.Self ? combat.actor : combat.target;
            effect.ApplyEffect(combat, target, usedAbility.abilityData.name, usedAbility.abilityData.icon);
        }

        // +1 adjustment, because after each turn all cooldowns are decreased by 1 (even for used ability)
        usedAbility.curCooldown = abilityData.cooldown + 1;
    }

    public void UseConsumable(Combat combat)
    {
        //LogEvent(combat, $"{combat.hero.name} used {consumableData.name} on {combat.target.name}.");
        var usedConsumable = combat.hero.consumables.First(cons => cons.consumableData == consumableData);
        foreach (var effect in usedConsumable.consumableData.effects)
        {
            var target = effect.target == Target.Self ? combat.actor : combat.target;
            effect.ApplyEffect(combat, target, usedConsumable.consumableData.name, usedConsumable.consumableData.icon);
        }

        // +1 adjustment, because after each turm all cooldowns are decreased by 1 (even on used ability)
        usedConsumable.curCharges--;
    }

    public void LogEvent(CombatManager combat, string text)
    {
        //combat.expedition.UpdateLog(text);
    }

    #endregion
}