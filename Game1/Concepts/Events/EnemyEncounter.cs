using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Objects.Units;

namespace Game1.Concepts
{
    public class EnemyEncounter : Event
    {
        public Hero Hero { get; set; }
        public Enemy Enemy { get; set; }
        public bool HeroTurn { get; set; }
        public int Turn { get; set; }

        public EnemyEncounter(Hero hero, Location location)
        {
            Hero = hero;
            Enemy = TrySpawnEnemy(location);

            Name = $"Battle with {Enemy.Name}.";
            Texture = Enemy.Texture;
            Globals.ExpeditionsDict[hero.ID].Enemy = Enemy;

            CombatPreparation();
        }

        // TODO: increase chance with each iteration?
        public static Enemy TrySpawnEnemy(Location location)
        {
            for (var tryIndex = 0; tryIndex < 10; tryIndex++)
            {
                foreach (var enemyData in location.LocationData.Enemies)
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
            if (HeroTurn)
            {
                Enemy.Health -= Math.Max(Hero.Attack - Enemy.Defence, 0);
            }
            else
            {
                Hero.Health -= Math.Max(Enemy.Attack - Hero.Defence, 0);
            }
        }

        // TODO: add equipment and effects into equation
        public void CombatPreparation()
        {
            Hero.Health = Hero.BaseStats[Stat.Health];
            Hero.Speed = Hero.BaseStats[Stat.Speed];
            Hero.Attack = Hero.BaseStats[Stat.Attack];
            Hero.Defence = Hero.BaseStats[Stat.Defence];

            Enemy.Health = Enemy.BaseStats[Stat.Health];
            Enemy.Speed = Enemy.BaseStats[Stat.Speed];
            Enemy.Attack = Enemy.BaseStats[Stat.Attack];
            Enemy.Defence = Enemy.BaseStats[Stat.Defence];

            // turn decider
            if (Enemy.Speed < Hero.Speed || Enemy.Speed == Hero.Speed && Globals.RNGesus.Next(2) == 1)
                HeroTurn = true;
        }
    }
}