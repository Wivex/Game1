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
        //combat.mis.StartAnimation(AnimationTrigger.Attack, combat.GetAnimManager(combat.curActor));

        var damTaken = combat.target.TakeDamage(combat.mis, new Damage(DamageType.Physical, combat.actor.Attack));

        //UIManager.CreateEffectAnimation(combat.mis, combat.curTarget, UIManager.meleeHitEffectPrefab);

        //combat.mis.UpdateLog($"{combat.curActor} attacks {combat.curTarget} for {dam} {damage.type} damage.");
    }

    public void UseAbility(Combat combat)
    {
        var selAbility = combat.actor.abilities.Find(ab => ab.data.name == ability);
        foreach (var effect in selAbility.data.effects)
        {
            effect.AddEffect(combat, selAbility.data.name, selAbility.data.icon);
        }

        // +1 adjustment, because after each turn all cooldowns are decreased by 1 (even for used ability)
        selAbility.curCooldown += 1;
    }

    public void UseConsumable(Combat combat)
    {
        //LogEvent(combat, $"{combat.hero.name} used {consumableData.name} on {combat.curTarget.name}.");
        var usedConsumable = combat.hero.consumables.First(cons => cons.data == consumableData);
        //foreach (var effect in usedConsumable.data.useEffects)
        //{
        //    effect.AddEffect(combat, usedConsumable.data.name, usedConsumable.data.icon);
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