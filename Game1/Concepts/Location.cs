using Game1.Engine;
using Game1.Objects;
using XMLData;

namespace Game1.Concepts
{
    public class Location : Entity
    {
        public LocationData XMLData { get; set; }

        public Location(string locationName)
        {
            Name = locationName;
            XMLData = DB.Locations[locationName].Item1;
            Texture = DB.Locations[locationName].Item2;
        }
    }
}