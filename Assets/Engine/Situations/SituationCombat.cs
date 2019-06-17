using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SituationCombat : Situation
{
    public Hero hero;
    public Enemy enemy;
    public Unit actor, target;

    public SituationCombat(Expedition expedition, List<EnemySpawnChance> enemies) : base(expedition)
    {
        hero = expedition.hero;
        enemy = SpawnEnemy(enemies);
        type = SituationType.EnemyEncounter;
        ResetAllCooldowns();
    }

    bool HeroTurnFirst => hero.stats[(int) StatType.Speed] >= enemy.stats[(int) StatType.Speed];

    // TODO: increase chance with each iteration?
    public Enemy SpawnEnemy(List<EnemySpawnChance> enemies)
    {
        var tries = 0;
        while (enemy == null && tries++ < 100)
        {
            foreach (var e in enemies)
            {
                if (Random.value < e.chance)
                    return new Enemy(e.enemyData);
            }
        }

        throw new Exception("Too many tries to spawn enemy");
    }

    public override void Update()
    {
        if (hero.Dead)
        {
            Kill(hero);
            // TODO: stop situation
        }
        else if (enemy.Dead)
        {
            Kill(enemy);
        }
        else
        {
            if (HeroTurnFirst)
            {
                actor = hero;
                target = enemy;
                CombatTick();
                actor = enemy;
                target = hero;
                CombatTick();
            }
            else
            {
                actor = enemy;
                target = hero;
                CombatTick();
                actor = hero;
                target = enemy;
                CombatTick();
            }
        }
    }

    public void CombatTick()
    {
        actor.curInitiative += actor.stats[(int) StatType.Speed].curValue * GameManager.instance.combatSpeed;
        if (actor.curInitiative >= Unit.reqInitiative)
        {
            actor.curInitiative = 0;
            ActorMove();
        }
    }

    public void ActorMove()
    {
        UpdateActorEffects();
        UpdateActorTactics();
        UpdateActorCooldowns();
    }

    public void UpdateActorEffects()
    {
        for (var i = actor.curEffects.Count - 1; i >= 0; i--)
            actor.curEffects[i].UpdateEffect();
    }

    public void UpdateActorTactics()
    {
        foreach (var tactic in actor.tacticsPreset.tactics)
        {
            // skip tactic if not all triggers are triggered
            if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(this)))
                continue;
            tactic.action.DoAction(this);
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

    public void Kill(Hero hero)
    {
        // TODO: wait to cart items
    }

    public void Kill(Enemy enemy)
    {
        // TODO: drop loot, give exp
    }
}