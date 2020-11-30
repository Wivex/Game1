using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionType
{
    Attack,
    UseAbility,
    UseConsumable,
    Flee,
    Wait
}

[Serializable]
public class TacticAction
{
    public const int APCost = 5;

    public ActionType actionType;
    [HideIfNotEnumValues("actionType", ActionType.UseConsumable)]
    public ItemData consumableData;
    [PopupValue("abilitiesNames", order = 1), HideIfNotEnumValues("actionType", ActionType.UseAbility)]
    public string ability;
    
    public void Apply(Combat combat)
    {
        switch (actionType)
        {
            case ActionType.Wait:
                Wait(combat);
                break;
            case ActionType.Flee:
                Flee(combat);
                break;
            case ActionType.Attack:
                ApplyAttack(combat);
                break;
            case ActionType.UseAbility:
                ApplyAbility(combat);
                break;
            case ActionType.UseConsumable:
                ApplyConsumable(combat);
                break;
        }
    }

    #region ACTIONS

    public void Wait(Combat combat)
    {
        //combat.EndCombat();
    }

    public void Flee(Combat combat)
    {
        //combat.EndCombat();
    }

    public void ApplyAttack(Combat combat)
    {
        combat.target.ApplyDamage(new Damage(DamageType.Physical, combat.actor.Attack));
    }

    public void ApplyAbility(Combat combat)
    {
        var curAbility = combat.actor.abilities.Find(ab => ab.data.name == ability);
        curAbility.ApplyDirectEffects(combat);
        curAbility.curCooldown = curAbility.data.cooldown;
        foreach (var effectData in curAbility.data.effectsOverTime)
        {
            var targetUnit = effectData.target == TargetType.Hero ? (Unit) combat.hero : combat.enemy;
            targetUnit.AddEffect(effectData);
        }
    }

    public void ApplyConsumable(Combat combat)
    {
        //LogEvent(combat, $"{combat.hero.name} used {consumableData.name} on {combat.curTarget.name}.");
        var usedConsumable = combat.hero.consumables.First(cons => cons.data == consumableData);
        //foreach (var effect in usedConsumable.data.useEffects)
        //{
        //    effect.ApplyEffect(combat, usedConsumable.data.name, usedConsumable.data.icon);
        //}

        // +1 adjustment, because after each turm all cooldowns are decreased by 1 (even on used ability)
        usedConsumable.charges--;
    }

    //public void LogEvent(CombatManager combat, string text)
    //{
    //    //combat.mission.UpdateLog(text);
    //}

    #endregion
}