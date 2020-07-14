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
    public ItemData consumableData;

    public void DoAction(EnemyEncounter enemyEncounter)
    {
        switch (actionType)
        {
            case ActionType.Flee:
                Flee(enemyEncounter);
                break;
            case ActionType.Attack:
                Attack(enemyEncounter);
                break;
            case ActionType.UseAbility:
                UseAbility(enemyEncounter);
                break;
            case ActionType.UseConsumable:
                UseConsumable(enemyEncounter);
                break;
        }
    }


    #region ACTIONS

    public void Flee(EnemyEncounter enemyEncounter)
    {
        //enemyEncounter.EndCombat();
    }

    public void Attack(EnemyEncounter enemyEncounter)
    {
        //enemyEncounter.mis.StartAnimation(AnimationTrigger.Attack, enemyEncounter.GetAnimManager(enemyEncounter.actor));

        var damTaken = enemyEncounter.target.TakeDamage(enemyEncounter.mis, new Damage(DamageType.Physical, enemyEncounter.actor.Attack));

        //UIManager.CreateEffectAnimation(enemyEncounter.mis, enemyEncounter.target, UIManager.meleeHitEffectPrefab);

        //enemyEncounter.mis.UpdateLog($"{enemyEncounter.actor} attacks {enemyEncounter.target} for {dam} {damage.type} damage.");
    }

    public void UseAbility(EnemyEncounter enemyEncounter)
    {
        var usedAbility = enemyEncounter.actor.abilities.Find(abil => abil.abilityData == abilityData);
        foreach (var effect in usedAbility.abilityData.effects)
        {
            effect.AddEffect(enemyEncounter, usedAbility.abilityData.name, usedAbility.abilityData.icon);
        }

        // +1 adjustment, because after each turn all cooldowns are decreased by 1 (even for used ability)
        usedAbility.curCooldown = abilityData.cooldown + 1;
    }

    public void UseConsumable(EnemyEncounter enemyEncounter)
    {
        //LogEvent(enemyEncounter, $"{enemyEncounter.hero.name} used {consumableData.name} on {enemyEncounter.target.name}.");
        var usedConsumable = enemyEncounter.hero.consumables.First(cons => cons.data == consumableData);
        //foreach (var effect in usedConsumable.data.useEffects)
        //{
        //    effect.AddEffect(enemyEncounter, usedConsumable.data.name, usedConsumable.data.icon);
        //}

        // +1 adjustment, because after each turm all cooldowns are decreased by 1 (even on used ability)
        usedConsumable.charges--;
    }

    //public void LogEvent(CombatManager enemyEncounter, string text)
    //{
    //    //enemyEncounter.mission.UpdateLog(text);
    //}

    #endregion
}