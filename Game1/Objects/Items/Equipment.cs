using System.Collections.Generic;
using Game1.Concepts;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Objects
{
    public class Equipment : Item
    {
        public override string DataClassPath => "Items/Equipment";

        public EquipmentData XMLData { get; set; }
        public string Slot { get; set; }
        public Dictionary<string, int> Stats { get; set; }

        public Equipment(string xmlDataPath)
        {
            var path = $"{DataClassPath}/{xmlDataPath}";
            XMLData = Globals.TryLoadData<EquipmentData>(path);
            Texture = Globals.TryLoadTexture(path);

            Name = XMLData.Name;
            Cost = XMLData.Cost;
            Slot = XMLData.Slot;
            Stats = new Dictionary<string, int>(XMLData.Stats);
        }
    }
}