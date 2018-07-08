using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<int, Hero> heroes;

    public float combatSpeed = 0.1f;
    public float gameSpeed = 1;

    public Hero hero;
    public Enemy enemy;

    //bool HeroTurnFirst => enemy.speed < hero.speed || enemy.speed == hero.speed && Random.value < 0.5f;

    void FixedUpdate()
    {
        //if (HeroTurnFirst)
        //{
        //    CombatTick(hero, enemy);
        //    CombatTick(enemy, hero);
        //}
        //else
        //{
        //    CombatTick(enemy, hero);
        //    CombatTick(hero, enemy);
        //}
    }

    //public void CombatTick(Unit actor, Unit target)
    //{
    //    actor.curInitiative += actor.speed * combatSpeed;
    //    if (actor.curInitiative >= actor.maxInitiative)
    //    {
    //        actor.curInitiative -= actor.maxInitiative;
    //        TakeAction(actor, target);
    //    }
    //}

    //// TODO: add equipment and effects into equation
    //public void TakeAction(Unit actor, Unit target)
    //{
    //    var actionTaken = false;
    //    //foreach (var ability in actor.Abilities)
    //    //{
    //    //    // use ability
    //    //    if (ability.Ready && !actionTaken)
    //    //    {
    //    //        ability.Use(actor, target);
    //    //        actionTaken = true;
    //    //    }
    //    //    else
    //    //        ability.Cooldown--;
    //    //}

    //    // attack
    //    if (!actionTaken)
    //        Attack(actor, target);
    //}

    //public void Attack(Unit actor, Unit target)
    //{
    //    //var damage = new Damage(DamageType.Physical,
    //    //    Math.Max(actor.Stats[Stat.Attack] - target.Stats[Stat.Defence], 0));
    //    //target.TakeDamage(damage);

    //    target.TakeDamage(actor.attack - target.defence);
    //}
}
