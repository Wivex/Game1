using System.Collections.Generic;
using Game1.Concepts;
using Game1.Engine;
using XMLData;

namespace Game1.Objects
{
    public class Consumable : Item
    {
        public override int MaxStackSize => 10;

        public ConsumableData XMLData { get; set; }

        public Consumable(string consumableName)
        {
            Name = consumableName;
            XMLData = DB.Consumables[consumableName].Item1;
            Texture = DB.Consumables[consumableName].Item2;
        }
    }
}