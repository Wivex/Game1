using System.Collections.Generic;
using Game1.Concepts;
using Game1.Engine;
using XMLData;

namespace Game1.Objects
{
    public class Equipment : Item
    {
        public EquipmentData XMLData { get; set; }

        public Equipment(string equipmentName)
        {
            Name = equipmentName;
            StackSize = 1;
            XMLData = DB.Equipment[equipmentName].Item1;
            Texture = DB.Equipment[equipmentName].Item2;
        }
    }
}