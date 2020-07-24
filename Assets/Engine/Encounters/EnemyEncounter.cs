using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class EnemyEncounter : Encounter
{
    internal Hero hero;
    internal Enemy enemy;
    internal Unit curActor, curTarget;
    
    List<Item> lootDrops;
    Item curLoot;

    bool looting;
    int heroInitiative;

    internal EnemyEncounter(Mission mis) : base(mis)
    {
        type = EncounterType.Enemy;
        hero = mis.hero;
        // enemy = new Enemy(mis.curZone.enemies.PickOne().enemyData);
    }

    internal override void NextAction()
    {
        if (looting)
            NextItem();
        else
            NexUnitAction();
    }

    // TODO: rework into AP based system (speed points)
    /// <summary>
    /// Determines turn order based on hero vs enemy speed difference. Substitution leftover accumulates, so that multiple turns of the same unit can happen based on SPD advantage.
    /// </summary>
    void NexUnitAction()
    {
        // extra hero turn check
        if (heroInitiative > hero.Speed)
            heroInitiative -= hero.Speed;
        // extra enemy turn check (substitution leftover is negative)
        else if (-heroInitiative > enemy.Speed)
            heroInitiative -= -enemy.Speed;
        // normal turn check
        else
            heroInitiative += hero.Speed - enemy.Speed;

        if (heroInitiative > 0 || heroInitiative == 0 && Random.value > 0.5f)
        {
            curActor = hero;
            curTarget = enemy;
        }
        else
        {
            curActor = enemy;
            curTarget = hero;
        }

        DoActorTurn();
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
        for (var i = curActor.effects.Count - 1; i >= 0; i--)
            curActor.effects[i].ProcEffect();
    }

    void DoActorAction()
    {
        foreach (var tactic in curActor.tactics)
        {
            // skip tactic if not all triggers are triggered
            if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(enemy)))
                continue;
            tactic.action.DoAction(this);
            break;
        }
    }

    void UpdateActorCooldowns()
    {
        foreach (var ability in curActor.abilities)
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