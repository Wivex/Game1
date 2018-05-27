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
            XMLData = DataBase.Locations[locationName].Item1;
            Texture = DataBase.Locations[locationName].Item2;
        }
    }
}