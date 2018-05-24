using System.Collections.Generic;
using Game1.Concepts;
using XMLData;

namespace Game1.Objects
{
    public class Consumable : Item
    {
        public override string DataClassPath => "Items/Consumables";

        public ConsumableData XMLData { get; set; }

        public List<string> Effects { get; set; }

        public Consumable(string xmlDataPath)
        {
            var path = $"{DataClassPath}/{xmlDataPath}";
            XMLData = Globals.TryLoadData<ConsumableData>(path);
            Texture = Globals.TryLoadTexture(path);

            Name = XMLData.Name;
            Cost = XMLData.Cost;
            Effects = new List<string>(XMLData.Effects);
        }
    }
}