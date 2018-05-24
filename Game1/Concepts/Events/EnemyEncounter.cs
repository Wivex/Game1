using System;
using Game1.Objects;
using Game1.Objects.Units;
using Game1.UI;
using Game1.UI.Panels;
using GeonBit.UI.Entities;

namespace Game1.Concepts
{
    public class EnemyEncounter : Event
    {
        public Hero Hero { get; set; }
        public Enemy Enemy { get; set; }
        public PanelExpedition ExpeditionPanel { get; set; }
        public bool HeroTurnFirst { get; set; }
        public int Turn { get; set; }
        public double APPerSecond { get; set; }

        public EnemyEncounter(Hero hero, Location location, PanelExpedition expeditionPanel)
        {
            Hero = hero;
            Enemy = TrySpawnEnemy(location);
            ExpeditionPanel = expeditionPanel;

            Name = $"Battle with {Enemy.Name}.";
            Texture = Enemy.Texture;
            Globals.ExpeditionsDict[hero.ID].Enemy = Enemy;

            APPerSecond = Globals.Game.TargetElapsedTime.TotalMilliseconds / 1000;

            InitCombat();
        }

        // TODO: increase chance with each iteration?
        public static Enemy TrySpawnEnemy(Location location)
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
                TakeTurn(Hero, Enemy);
                TakeTurn(Enemy, Hero);
            }
            else
            {
                TakeTurn(Enemy, Hero);
                TakeTurn(Hero, Enemy);
            }
        }

        // TODO: add equipment and effects into equation
        public void TakeTurn(Unit actor, Unit target)
        {
            actor.ActionPoints += actor.Speed * APPerSecond;
            if (actor.ActionPoints >= actor.ActionCost)
            {
                actor.ActionPoints -= actor.ActionCost;
                var damage = Math.Max(actor.Attack - target.Defence, 0);
                target.Health -= damage;

                var targetPanel = actor is Hero ? ExpeditionPanel.EventImagePanel : ExpeditionPanel.HeroImagePanel;

                new FloatingText("-" + damage, targetPanel, TimeSpan.FromSeconds(3));
            }
        }

        // TODO: add equipment and effects into equation
        public void InitCombat()
        {
            // turn order roll
            if (Enemy.Speed < Hero.Speed || Enemy.Speed == Hero.Speed && Globals.RNGesus.Next(2) == 1)
                HeroTurnFirst = true;
        }
    }
}