using System;
using Game1.Objects;
using Game1.Objects.Units;
using Game1.UI.Panels;

namespace Game1.Concepts
{
    public class EnemyEncounter : Event
    {
        public Hero Hero { get; set; }
        public Enemy Enemy { get; set; }
        public PanelExpeditionOverview ExpeditionOverviewPanel { get; set; }

        public bool HeroTurnFirst => Enemy.Stats[Stat.Speed] < Hero.Stats[Stat.Speed]
                                     || Enemy.Stats[Stat.Speed] == Hero.Stats[Stat.Speed] &&
                                     Globals.RNGesus.Next(2) == 1;

        /// <summary>
        /// 1 AP per second
        /// </summary>
        public double BaseAPGainRate =>
            Globals.Game.TargetElapsedTime.TotalMilliseconds / 1000 * Globals.GameSpeedMultiplier;

        public EnemyEncounter(Hero hero, Location location, PanelExpeditionOverview expeditionOverviewPanel)
        {
            ExpeditionOverviewPanel = expeditionOverviewPanel;
            Hero = hero;
            Enemy = TrySpawnEnemy(location);
            Enemy.UnitPanel = ExpeditionOverviewPanel.EventImagePanel;

            Name = $"Battle with {Enemy.Name}.";
            Texture = Enemy.Texture;
            Globals.ExpeditionsDict[hero.ID].Enemy = Enemy;
        }

        // TODO: increase chance with each iteration?
        public Enemy TrySpawnEnemy(Location location)
        {
            for (var tryIndex = 0; tryIndex < 100; tryIndex++)
            {
                foreach (var enemyData in location.XMLData.Enemies)
                {
                    if (Globals.RNGesus.NextDouble() < enemyData.Value)
                        return new Enemy(enemyData.Key);
                }
            }

            throw new Exception("Generating enemy takes too many tries");
        }

        /// <summary>
        /// Combat simulation
        /// </summary>
        public override void Update()
        {
            if (HeroTurnFirst)
            {
                CombatTick(Hero, Enemy);
                CombatTick(Enemy, Hero);
            }
            else
            {
                CombatTick(Enemy, Hero);
                CombatTick(Hero, Enemy);
            }
        }

        public void CombatTick(Unit actor, Unit target)
        {
            actor.UpdateEffects();

            actor.ActionPoints += actor.Stats[Stat.Speed].Value * BaseAPGainRate;
            if (actor.ActionPoints >= actor.ActionCost)
            {
                actor.ActionPoints -= actor.ActionCost;
                TakeAction(actor, target);
            }
        }

        // TODO: add equipment and effects into equation
        public void TakeAction(Unit actor, Unit target)
        {
            var actionTaken = false;
            foreach (var ability in actor.Abilities)
            {
                // use ability
                if (ability.Ready && !actionTaken)
                {
                    ability.Use(actor, target);
                    actionTaken = true;
                }
                else
                    ability.Cooldown--;
            }

            // attack
            if (!actionTaken)
                Attack(actor, target);
        }

        public void Attack(Unit actor, Unit target)
        {
            var damage = new Damage(DamageType.Physical,
                Math.Max(actor.Stats[Stat.Attack] - target.Stats[Stat.Defence], 0));
            target.TakeDamage(damage);
        }
    }
}