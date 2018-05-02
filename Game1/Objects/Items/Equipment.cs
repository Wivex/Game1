using System.Collections.Generic;
using Game1.Concepts;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Objects
{
    public class Equipment : Item
    {
        public EquipmentData EquipmentData { get; set; }
        public string SlotName { get; set; }
        public Dictionary<string, int> Stats { get; set; }

        public Equipment(string name)
        {
            EquipmentData = Globals.Game.Content.Load<EquipmentData>(@"Settings/Items/Equipment/" + name);

            Name = name;
            Texture = Globals.TryLoadTexture(@"Textures/Items/Equipment/", EquipmentData.TextureName);
            SlotName = EquipmentData.SlotName;
            Stats = new Dictionary<string, int>(EquipmentData.Stats);
        }
    }
}