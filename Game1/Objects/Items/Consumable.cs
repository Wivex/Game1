using System.Collections.Generic;
using Game1.Concepts;
using Game1.Engine;
using XMLData;

namespace Game1.Objects
{
    public class Consumable : Item
    {
        public ConsumableData XMLData { get; set; }

        public Consumable(string consumableName)
        {
            Name = consumableName;
            Stacksize = 10;
            XMLData = DataBase.Consumables[consumableName].Item1;
            Texture = DataBase.Consumables[consumableName].Item2;
        }
    }
}