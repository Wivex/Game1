using UnityEngine;

public class SituationCombat : Situation
{
    public Hero hero;
    public Enemy enemy;

    public SituationCombat(Hero hero, EnemySpawnChance[] enemies)
    {
        this.hero = hero;
        enemy = SpawnEnemy(enemies);
        type = SituationType.EnemyEncounter;
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
        if (HeroTurnFirst)
        {
            CombatTick(hero, enemy);
            CombatTick(enemy, hero);
        }
        else
        {
            CombatTick(enemy, hero);
            CombatTick(hero, enemy);
        }
    }

    public void CombatTick(Unit actor, Unit target)
    {
        actor.curInitiative += actor.stats[(int) StatType.Speed] * ExpeditionManager.combatSpeed;
        if (actor.curInitiative >= Unit.reqInitiative)
        {
            actor.curInitiative -= Unit.reqInitiative;
            TakeAction(actor, target);
        }
    }

    // TODO: add tactics implementation
    public void TakeAction(Unit actor, Unit target)
    {
        var actionTaken = false;
        //foreach (var ability in actor.Abilities)
        //{
        //    // use ability
        //    if (ability.Ready && !actionTaken)
        //    {
        //        ability.Use(actor, target);
        //        actionTaken = true;
        //    }
        //    else
        //        ability.Cooldown--;
        //}

        // attack
        if (!actionTaken)
            Attack(actor, target);
    }

    public void Attack(Unit actor, Unit target)
    {
        target.TakeDamage(new Damage(DamageType.Physical,
            actor.stats[(int) StatType.Attack] - target.stats[(int) StatType.Defence]));
    }
}