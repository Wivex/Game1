using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI.GeonUI_Overrides
{
    public class IconNew : Icon
    {
        public IconNew(Texture2D texture, Vector2 size) : base(IconType.None, Anchor.Center, 1, false, null)
        {
            Texture = texture;
            Size = size;
        }

        public IconNew(Vector2 size) : base(IconType.None, Anchor.Center, 1, false, null)
        {
            Size = size;
        }
    }
}