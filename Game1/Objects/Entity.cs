using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Objects
{
    public abstract class Entity
    {
        public string name { get; set; }
        public Texture2D Texture { get; set; }
    }
}
