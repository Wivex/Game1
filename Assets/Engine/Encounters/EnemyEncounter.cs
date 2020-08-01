using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class EnemyEncounter : NoEncounter
{
    internal const int AP_AccumulationLimitMod = 2;

    internal Enemy enemy;
    internal Unit fasterUnit, actor, target;
    
    List<Item> lootDrops;
    Item curLoot;

    bool looting;
    int heroAP, enemyAP;

    #region EVENTS

    internal event Action NextUnitTurn;

    #endregion

    internal EnemyEncounter(Mission mis) : base(mis)
    {
        type = EncounterType.Enemy;
        // TODO used mission one
        enemy = new Enemy(mis.route.curZone.enemies.PickOne().enemyData);
    }

    internal override void NextUpdate()
    {
        if (!looting)
            NextUnitAction();
        else
            NextItem();
    }

    // TODO: simultaneous turn if same AP?
    void NewTurn()
    {
        heroAP = Math.Min(heroAP + hero.Speed, hero.Speed * AP_AccumulationLimitMod);
        enemyAP = Math.Min(enemyAP + enemy.Speed, enemy.Speed * AP_AccumulationLimitMod);

        fasterUnit = heroAP >= enemyAP ? (Unit) hero : enemy;
        //actor = heroAP >= enemyAP ? (Unit)hero : enemy;
        //target = heroAP >= enemyAP ? (Unit)enemy : hero;
    }

    void NextUnitAction()
    {
        
        // if can use any action
        // do action
        // else next turn
    }

    void TryEndCombat()
    {
        if (hero.Dead || enemy.Dead)
            EndCombat();
    }

    void EndCombat()
    {
        // loose condition
        if (hero.Dead)
        {
            //UNDONE
        }
        // win condition
        else
        {
            SpawnLoot();
            looting = true;
        }
    }

    void NextItem()
    {
        if (lootDrops.Any())
        {
            // TODO: use Queue or Stack instead
            curLoot = lootDrops.ExtractFirstElement();
            hero.backpack.Add(curLoot);
            //mis.StartAnimation(AnimationTrigger.StartTransferLoot, mis.lootAM, mis.interactionAM, mis.locationAM);
        }
        else
        {
            looting = false;
            //mis.StartAnimation(AnimationTrigger.EndEncounter, mis.heroAM, mis.encounterAM);
            mis.curEncounter = null;
        }
    }

    void DoActorTurn()
    {
        UpdateActorEffects();
        // can die from DoT
        TryEndCombat();
        DoActorAction();
        // can die from action
        TryEndCombat();
        UpdateActorCooldowns();
    }

    void UpdateActorEffects()
    {
        for (var i = actor.effects.Count - 1; i >= 0; i--)
            actor.effects[i].ProcEffect();
    }

    void DoActorAction()
    {
        //foreach (var tactic in actor.tactics)
        //{
        //    // skip tactic if not all triggers are triggered
        //    if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(enemy)))
        //        continue;
        //    tactic.action.DoAction(this);
        //    break;
        //}
    }

    void UpdateActorCooldowns()
    {
        foreach (var ability in actor.abilities)
        {
            if (ability.curCooldown > 0)
                ability.curCooldown--;
        }
    }

    void ResetAllCooldowns()
    {
        foreach (var ability in hero.abilities) ability.curCooldown = 0;
        foreach (var ability in enemy.abilities) ability.curCooldown = 0;
    }

    //UNDONE
    void SpawnLoot()
    {
        lootDrops = new List<Item>();
        foreach (var loot in enemy.data.lootTable)
        {
            // TODO: add stack count implementation
            if (Random.value < loot.dropChance)
            {
                lootDrops.Add(new Item(loot.item));
            }
        }
    }
}