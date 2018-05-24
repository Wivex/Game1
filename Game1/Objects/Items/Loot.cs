using Game1.Concepts;
using XMLData;

namespace Game1.Objects
{
    public class Loot : Item
    {
        public override string DataClassPath => "Items/Loot";

        public ItemData XMLData { get; set; }

        public Loot(string xmlDataPath)
        {
            var path = $"{Globals.DataPathBase}/{DataClassPath}/{xmlDataPath}";
            XMLData = Globals.TryLoadData<ItemData>(path);
            Texture = Globals.TryLoadTexture(path);

            Name = XMLData.Name;
            Cost = XMLData.Cost;
        }
    }
}