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
    [StringInList("abilitiesNames", order = 1), HideIfNotEnumValues("actionType", ActionType.UseAbility)]
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
                UseAbility(combat);
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

    public void UseAbility(Combat combat)
    {
        var curAbility = combat.actor.abilities.Find(ab => ab.data.name == ability);
        foreach (var effectData in curAbility.data.effects)
        {
            var targetUnit = effectData.target == TargetType.Hero ? (Unit) combat.hero : combat.enemy;
            if (effectData.procType == ProcType.Instant)
            {
                new Effect(effectData).ProcEffect(combat.mis, targetUnit);
            }
            else
            {
                targetUnit.effects.Add(new Effect(effectData));
            }
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

        



        internal void ApplyEffect(Combat combat, string sourceName, Sprite sourceIcon)
        {
            name = sourceName;
            icon = sourceIcon;
            curDuration = duration;

            targetUnit = target == TargetType.Hero ? combat.actor : combat.target;

            if (duration > 1)
            {
                targetUnit.effects.Add(this);
            }
            else
                ProcEffect();
        }
    }

    //public void LogEvent(CombatManager combat, string text)
    //{
    //    //combat.mission.UpdateLog(text);
    //}

    #endregion
}