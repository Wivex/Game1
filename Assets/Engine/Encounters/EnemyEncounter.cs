using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

internal enum CombatPhase
{
    DoTs,
    NewTurn,
    FasterUnitTurn,
    SlowerUnitTurn,
    Looting
}

public class EnemyEncounter : NoEncounter
{
    internal const int AP_AccumulationLimitMod = 2;

    internal Enemy enemy;
    internal Unit actor, target;
    
    List<Item> lootDrops;
    Item curLoot;

    int heroAP, enemyAP;
    CombatPhase phase = CombatPhase.DoTs;

    #region EVENTS

    //internal event Action NextUnitTurn;

    #endregion

    internal EnemyEncounter(Mission mis) : base(mis)
    {
        type = EncounterType.Enemy;
        // TODO used mission one
        enemy = new Enemy(mis.route.curZone.enemies.PickOne().enemyData);
        NewTurnSetUp();
    }

    internal override void NextUpdate()
    {
        // Check if any unit dead or flee
        CombatEndCheck();

        switch (phase)
        {
            case CombatPhase.DoTs:
                // Apply DoTs (call DoTs animation effects)
                // Death check
                NewCombatTurn();
                break;
            case CombatPhase.Looting:
                NextItemDrop();
                break;
            default: 
                NextUnitAction();
                break;
        }
    }

    void NewTurnSetUp()
    {
        // new turn updates
        phase = CombatPhase.DoTs;
    }

    void NewCombatTurn()
    {
        heroAP = Math.Min(heroAP + hero.Speed, hero.Speed * AP_AccumulationLimitMod);
        enemyAP = Math.Min(enemyAP + enemy.Speed, enemy.Speed * AP_AccumulationLimitMod);
        actor = heroAP >= enemyAP ? (Unit) hero : enemy;
        target = heroAP >= enemyAP ? (Unit) enemy : hero;

        phase = CombatPhase.FasterUnitTurn;
        NextUnitAction();
    }

    void NextUnitAction()
    {
        var actorAction = actor.tactics.PickAction(this);
        if (actorAction.actionType == ActionType.Wait)
        {
            // if faster unit can't do more actions
            if (phase == CombatPhase.FasterUnitTurn)
            {
                // slower unit turn to act
                phase = CombatPhase.SlowerUnitTurn;
                // switch roles
                var actorRole = actor;
                actor = target;
                target = actorRole;
            }
            else
                // Combat turn ended, prep for next
                NewTurnSetUp();
        }
        else
        {
            // perform appropriate tactic action
            actorAction.Perform(this);
        }
    }


    void CombatEndCheck()
    {
        // Mission tactics (flee) go here
        
        //if (hero.shouldFlee)
        //{
        //    //flee
        //}
        if (hero.Dead)
        {
            // hero dead smth
        }
        else if (enemy.Dead)
            StartLooting();
    }

    void StartLooting()
    {
        SpawnLoot();
        phase = CombatPhase.Looting;
    }

    void NextItemDrop()
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
            resolved = true;
        }
    }

    void DoActorTurn()
    {
        UpdateActorEffects();
        // can die from DoT
        CombatEndCheck();
        DoActorAction();
        // can die from action
        CombatEndCheck();
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
        //    if (tactic.triggers.Exists(trigger => !trigger.Triggered(enemy)))
        //        continue;
        //    tactic.action.Perform(this);
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