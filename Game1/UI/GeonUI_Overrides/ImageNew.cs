using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI.GeonUI_Overrides
{
    public class ImageNew : Image
    {
        public ImageNew(Texture2D texture, Vector2 size) : base(texture, size, ImageDrawMode.Stretch, Anchor.Center, null)
        {
            // initializtion required to make it being drawn
            ToolTipText = default(string);
        }
    }
}