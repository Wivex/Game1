using System;
using System.Collections.Generic;
using Game1.Concepts;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Objects.Units
{
    public class Enemy : Unit
    {
        public EnemyData EnemyData { get; set; }
        public int Experience { get; set; }

        public Enemy(string name)
        {
            EnemyData = Globals.Game.Content.Load<EnemyData>(@"Settings/Enemies/" + name);

            Name = name;
            Texture = Globals.TryLoadTexture(@"Textures/Enemies/", EnemyData.TextureName);
            Stats = new Dictionary<string, int>(EnemyData.Stats);
        }

        public void DropLoot(Hero hero)
        {
            var rng = new Random();
            foreach (var item in EnemyData.DropTable)
            {

                if (rng.NextDouble() < item.Value)
                {
                    //hero.Inventory.Add(new Item());
                }
            }
        }
    }
}