﻿using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class EnemyEncounter : Encounter
{
    internal Hero hero;
    internal Enemy enemy;
    internal Unit actor, target;
    internal Item curLoot;

    // UNDONE
    List<Item> lootDrops;

    bool looting;
    int heroInitiative;

    internal AnimatorManager GetAnimManager(Unit unit) => unit is Hero ? mis.heroAM : mis.encounterAM;

    internal EnemyEncounter(Mission mis) : base(mis)
    {
        type = EncounterType.Enemy;
        hero = mis.hero;
        enemy = NewEnemy();
        //TODO: update UI?
        //enemy.unitPreviewIcon = mission.expPreviewPanel.objectIcon.transform;
        //enemy.unitDetailsIcon = UIManager.instance.expPanelDrawer.expDetailsPanelDrawer.enemyPanel.unitImage.transform;
        ResetAllCooldowns();
    }

    internal override void NextStage()
    {
        if (looting)
            NextItem();
        else
            NextTurn();
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
            mis.StartAnimation(AnimationTrigger.StartTransferLoot, mis.lootAM, mis.interactionAM, mis.locationAM);
        }
        else
        {
            looting = false;
            mis.StartAnimation(AnimationTrigger.EndEncounter, mis.heroAM, mis.encounterAM);
            mis.curEncounter = null;
        }
    }

    /// <summary>
    /// Determines turn order based on hero vs enemy speed difference. Substitution leftover accumulates, so that multiple turns of the same unit can happen based on SPD advantage.
    /// </summary>
    void NextTurn()
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
            actor = hero;
            target = enemy;
        }
        else
        {
            actor = enemy;
            target = hero;
        }

        DoActorTurn();
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
        foreach (var tactic in actor.tactics)
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

    // NOTE: move to enemyEncounter manager?
    // NOTE: increase chance with each iteration?
    Enemy NewEnemy()
    {
        // var spawnTries = 0;
        // while (enemy == null && spawnTries++ < 100)
        // {
        //     foreach (var e in mis.curZone.enemies)
        //     {
        //         if (Random.value < e.chanceWeight)
        //             return new Enemy(e.enemyData);
        //     }
        // }

        throw new Exception("Too many tries to spawn enemy");
    }
}