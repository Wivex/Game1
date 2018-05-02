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

        public EnemyEncounter(Hero hero, Location location)
        {
            Hero = hero;
            Enemy = TrySpawnEnemy(location);

            Name = string.Format("Battle with {0}.", Enemy.Name);
            Globals.Expeditions[hero].Enemy = Enemy;
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

        }
    }
}