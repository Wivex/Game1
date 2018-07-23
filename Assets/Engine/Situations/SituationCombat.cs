using UnityEngine;

public class SituationCombat : Situation
{
    public Hero hero;
    public Enemy enemy;
    public Unit actor,target;

    public SituationCombat(Expedition expedition, EnemySpawnChance[] enemies) : base(expedition)
    {
        hero = expedition.hero;
        enemy = SpawnEnemy(enemies);
        type = SituationType.EnemyEncounter;
        expedition.expeditionPanel.UpdateLog($"Fighting with {enemy.enemyData.name}");
    }

    bool HeroTurnFirst => hero.stats[(int)StatType.Speed] >= enemy.stats[(int)StatType.Speed];

    // TODO: increase chance with each iteration?
    public Enemy SpawnEnemy(EnemySpawnChance[] enemies)
    {
        var tries = 0;
        while (enemy == null)
        {
            foreach (var e in enemies)
            {
                if (Random.value < e.chance)
                {
                    Debug.Log($"Tries to spawn {e.enemyData.name}: {tries}");
                    return new Enemy(e.enemyData);
                }
            }

            tries++;
        }

        return null;
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
            readyForNewSituation = true;
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
        actor.curInitiative += actor.stats[(int)StatType.Speed].curValue * GameManager.combatSpeed;
        if (actor.curInitiative >= Unit.reqInitiative)
        {
            actor.curInitiative = 0;
            ActorMove();
        }
    }

    public void ActorMove()
    {
        UpdateActorCooldowns();
        UpdateActorEffects();
        foreach (var tactic in actor.tacticsPreset.tactics)
        {
            // skip tactic if not all triggers are triggered
            if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(hero, enemy, actor)))
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

    public void UpdateActorEffects()
    {
        var effects = actor.curEffects;
        for (var i = effects.Count - 1; i >= 0; i--)
        {
            effects[i].curDuration--;
            if (effects[i].curDuration == 0)
                effects.RemoveAt(i);
        }
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