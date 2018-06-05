using Game1.Concepts;
using Game1.Engine;
using XMLData;

namespace Game1.Objects
{
    public class Loot : Item
    {
        public override int MaxStackSize => 99;

        public ItemData XMLData { get; set; }

        public Loot(string lootName)
        {
            Name = lootName;
            StackSize = 99;
            XMLData = DB.Loot[lootName].Item1;
            Texture = DB.Loot[lootName].Item2;
        }
    }
}