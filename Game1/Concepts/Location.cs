using Game1.Objects;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Concepts
{
    public class Location : Entity
    {
        public override string DataClassPath => "Locations&Events";

        public LocationData XMLData { get; set; }

        public Location(string xmlDataPath)
        {
            var path = $"{DataClassPath}/{xmlDataPath}";
            XMLData = Globals.TryLoadData<LocationData>(path);
            Texture = Globals.TryLoadTexture(path);

            Name = XMLData.Name;
        }
    }
}