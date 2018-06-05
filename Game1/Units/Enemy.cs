using Game1.Concepts;
using Game1.Engine;
using Game1.Mechanics;
using Game1.UI;
using XMLData;

namespace Game1.Objects.Units
{
    public class Enemy : Unit
    {
        public EnemyData XMLData { get; set; }

        //public int MaxHealth => XMLData.Stats[Stat.Health];

        public Enemy(string enemyName)
        {
            Name = enemyName;
            XMLData = DB.Enemies[enemyName].Item1;
            Texture = DB.Enemies[enemyName].Item2;

            foreach (var statData in XMLData.Stats)
                Stats.Add(statData.Key, new Stat(statData.Key, statData.Value));

            foreach (var abilityName in XMLData.Abilities)
                Abilities.Add(new Ability(abilityName));
        }

        //public void DropLoot(Hero hero)
        //{
        //    var rng = new Random();
        //    foreach (var item in XMLData.DropTable)
        //    {

        //        if (rng.NextDouble() < item.Value)
        //        {
        //            //hero.Inventory.Add(new Item());
        //        }
        //    }
        //}
    }
}