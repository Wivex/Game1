using Game1.Concepts;
using Game1.Engine;
using XMLData;

namespace Game1.Objects
{
    public class Loot : Item
    {
        public ItemData XMLData { get; set; }

        public Loot(string lootName)
        {
            Name = lootName;
            Stacksize = 99;
            XMLData = DataBase.Loot[lootName].Item1;
            Texture = DataBase.Loot[lootName].Item2;
        }
    }
}