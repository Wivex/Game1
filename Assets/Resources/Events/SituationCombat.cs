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

    bool HeroTurnFirst => enemy.stats[(int) StatType.Speed] < hero.stats[(int) StatType.Speed] ||
                          enemy.stats[(int) StatType.Speed] == hero.stats[(int) StatType.Speed] && Random.value < 0.5f;

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
            TakeAction(actor, target);
        }
    }

    public void TakeAction(Unit actor, Unit target)
    {
        foreach (var tactic in actor.tacticsPreset.tactics)
        {
            // skip tactic if not all triggers are triggered
            if (tactic.triggers.Exists(trigger => !trigger.IsTriggered(hero, enemy, actor)))
                continue;
            tactic.action.DoAction(this);
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