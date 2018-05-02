using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Objects;
using Microsoft.Xna.Framework.Graphics;
using XMLData;

namespace Game1.Concepts
{
    public class Location : Entity
    {
        public LocationData LocationData { get; set; }

        public Location(string name)
        {
            LocationData = Globals.Game.Content.Load<LocationData>(@"Settings/Locations&Events/" + name);

            Name = name;
            Texture = Globals.TryLoadTexture(@"Textures/Locations&Events/",LocationData.TextureName);
        }
    }
}