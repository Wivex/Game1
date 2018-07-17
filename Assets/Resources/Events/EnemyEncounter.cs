public class EnemyEncounter : Event
{
    public Hero hero;
    public Enemy enemy;

    // TODO: increase chance with each iteration?
    public Enemy TrySpawnEnemy(LocationData location)
    {
        for (var tryIndex = 0; tryIndex < 100; tryIndex++)
        {
            //foreach (var enemyData in location.XMLData.Enemies)
            //{
            //    //if (Globals.RNGesus.NextDouble() < enemyData.Value)
            //    //    return new Enemy(enemyData.Key);
            //}
        }

        //throw new Exception("Generating enemy takes too many tries");
        return null;
    }

    //bool HeroTurnFirst => enemy.speed < hero.speed || enemy.speed == hero.speed && Random.value < 0.5f;

    public override void Update()
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
    //    //    Math.Max(actor.Stat[Stat.Attack] - target.Stat[Stat.Defence], 0));
    //    //target.TakeDamage(damage);

    //    target.TakeDamage(actor.attack - target.defence);
    //}
}