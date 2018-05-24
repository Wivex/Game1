using System;
using System.Collections.Generic;
using Game1.Concepts;
using Game1.Mechanics;
using XMLData;

namespace Game1.Objects.Units
{
    public class Enemy : Unit
    {
        public override string DataClassPath => "Enemies";

        public EnemyData XMLData { get; set; }

        public Enemy(string xmlDataPath)
        {
            var path = $"{DataClassPath}/{xmlDataPath}";
            XMLData = Globals.TryLoadData<EnemyData>(path);
            Texture = Globals.TryLoadTexture(path);

            Name = XMLData.Name;

            Health = XMLData.Stats[Stat.Health];
            Attack = XMLData.Stats[Stat.Attack];
            Defence = XMLData.Stats[Stat.Defence];
            Resistance = XMLData.Stats[Stat.Resistance];
            Speed = XMLData.Stats[Stat.Speed];

            foreach (var abilityData in XMLData.Abilities)
            {
                Abilities.Add(new Ability(abilityData.Name));
            }
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