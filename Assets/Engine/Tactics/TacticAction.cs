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
    public ActionType actionType;
    [HideIfNotEnumValues("actionType", ActionType.UseConsumable)]
    public ItemData consumableData;
    [PopupValue("abilitiesNames", order = 1), HideIfNotEnumValues("actionType", ActionType.UseAbility)]
    public string ability;
    
    public void Perform(Combat combat)
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
                Attack(combat);
                break;
            case ActionType.UseAbility:
                ApplyAbilityEffects(combat);
                break;
            case ActionType.UseConsumable:
                UseConsumable(combat);
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

    public void Attack(Combat combat)
    {
        combat.mis.ApplyDamage(combat.target, new Damage(DamageType.Physical, combat.actor.Attack));
    }

    public void ApplyAbilityEffects(Combat combat)
    {
        var curAbility = combat.actor.abilities.Find(ab => ab.data.name == ability);
        curAbility.ApplyDirectEffects(combat);
        foreach (var effectParams in curAbility.data.effectsOverTime)
        {
            var targetUnit = effectParams.target == TargetType.Hero ? (Unit) combat.hero : combat.enemy;
            combat.AddEffects(targetUnit, new EffectOverTime(effectParams));
        }
    }

    public void UseConsumable(Combat combat)
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