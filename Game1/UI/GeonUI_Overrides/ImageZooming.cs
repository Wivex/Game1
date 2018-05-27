using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI.GeonUI_Overrides
{
    public class ImageZooming : Icon
    {
        public static int BorderPanelWidth { get; set; } = 5;

        public ImageZooming(Vector2 size, bool drawBorderPanel = false) : base(IconType.None, Anchor.Center, 1, drawBorderPanel, null)
        {
            Size = drawBorderPanel ? size - new Vector2(BorderPanelWidth, BorderPanelWidth)*2 : size;
            // initializtion required to make it being drawn
            ToolTipText = default(string);
        }

        public ImageZooming(Texture2D texture, Vector2 size, bool drawBorderPanel = false) : this(size, drawBorderPanel)
        {
            Texture = texture;
        }
    }
}