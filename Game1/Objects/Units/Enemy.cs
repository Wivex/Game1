using Game1.Concepts;
using Game1.Engine;
using Game1.Mechanics;
using XMLData;

namespace Game1.Objects.Units
{
    public class Enemy : Unit
    {
        public EnemyData XMLData { get; set; }

        public Enemy(string enemyName)
        {
            Name = enemyName;
            XMLData = DataBase.Enemies[enemyName].Item1;
            Texture = DataBase.Enemies[enemyName].Item2;

            Health = XMLData.Stats[Stat.Health];
            Attack = XMLData.Stats[Stat.Attack];
            Defence = XMLData.Stats[Stat.Defence];
            Resistance = XMLData.Stats[Stat.Resistance];
            Speed = XMLData.Stats[Stat.Speed];

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