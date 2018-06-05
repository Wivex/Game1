using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.GeonUI_Overrides
{
    public class ProgressBarNew : ProgressBar
    {
        public ProgressBarNew(Vector2 size, Anchor anchor = Anchor.Auto, Vector2? offset = null) : base(0, 100, size, anchor, offset)
        {
            // offset to center the caption text
            Caption.SetOffset(Vector2.UnitY);
        }

        protected override void DoOnMouseWheelScroll() { }
        protected override void DoWhileMouseDown() { }
    }
}