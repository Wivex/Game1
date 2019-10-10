using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Combat : Encounter
{
    internal Hero hero;
    internal Enemy enemy;
    internal Unit actor, target;

    List<ItemData> lootDrops;

    bool looting;
    int heroInitiative;

    AnimatorManager GetAnimManager(Unit unit) => unit is Hero ? exp.heroAM : exp.objectAM;

    public Combat(Expedition exp) : base(exp)
    {
        type = EncounterType.Combat;
        hero = exp.hero;
        enemy = NewEnemy();
        //TODO: update UI?
        //enemy.unitPreviewIcon = expedition.expPreviewPanel.objectIcon.transform;
        //enemy.unitDetailsIcon = UIManager.instance.expPanelDrawer.expDetailsPanelDrawer.enemyPanel.unitImage.transform;
        ResetAllCooldowns();
    }

    internal override void Update()
    {
        if (hero.Dead)
        {
            hero.Kill();
        }
        else if (enemy.Dead)
        {
            enemy.Kill();
        }
        else
        {
            NextTurn();
        }
    }

    /// <summary>
    /// Determines turn order based on hero vs enemy speed difference. Substitution leftover accumulates, so that multiple turns of the same unit can happen based on SPD advantage.
    /// </summary>
    public void NextTurn()
    {
        // extra hero turn check
        if (heroInitiative > hero.curStats.speed)
            heroInitiative -= hero.curStats.speed;
        // extra enemy turn check (substitution leftover is negative)
        else if (-heroInitiative > enemy.curStats.speed)
            heroInitiative -= -enemy.curStats.speed;
        // normal turn check
        else
            heroInitiative += hero.curStats.speed - enemy.curStats.speed;

        actor = heroInitiative > 0 ? (Unit) hero : enemy;
        target = heroInitiative > 0 ? (Unit) enemy : hero;

        DoActorTurn();
    }

    public void DoActorTurn()
    {
        UpdateActorEffects();
        DoActorAction();
        UpdateActorCooldowns();
    }

    public void UpdateActorEffects()
    {
        for (var i = actor.effects.Count - 1; i >= 0; i--)
            actor.effects[i].UpdateEffect();
        if (actor.Dead)
        {
            actor.Kill();
        }
    }

    public void DoActorAction()
    {
        foreach (var tactic in actor.tacticsPreset.tactics)
        {
            // skip tactic if not all triggers are triggered
            if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(enemy)))
                continue;
            tactic.action.DoAction(this);
            exp.StartAnimation(AnimationTrigger.Attack, GetAnimManager(actor));
            break;
        }
    }

    public void UpdateActorCooldowns()
    {
        foreach (var ability in actor.abilities)
        {
            if (ability.curCooldown > 0)
                ability.curCooldown--;
        }
    }

    public void ResetAllCooldowns()
    {
        foreach (var ability in hero.abilities) ability.curCooldown = 0;
        foreach (var ability in enemy.abilities) ability.curCooldown = 0;
    }

    public void SpawnLoot()
    {
        lootDrops = new List<ItemData>();
        foreach (var loot in enemy.enemyData.lootTable)
        {
            // TODO: add stack count implementation
            if (Random.value < loot.dropChance)
                lootDrops.Add(loot.item);
        }
    }

    // NOTE: move to combat manager?
    // NOTE: increase chance with each iteration?
    Enemy NewEnemy()
    {
        var spawnTries = 0;
        while (enemy == null && spawnTries++ < 100)
        {
            foreach (var e in exp.curLocation.enemies)
            {
                if (Random.value < e.chance)
                    return new Enemy(e.enemyData);
            }
        }

        throw new Exception("Too many tries to spawn enemy");
    }
}