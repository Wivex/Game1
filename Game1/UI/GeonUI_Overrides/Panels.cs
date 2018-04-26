using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI
{
    /// <summary>
    /// Sugarcoat to implement SizeInternal property for GeonUI Panel class
    /// </summary>
    public class PanelEmpty : Panel
    {
        /// <summary>
        /// Padding works as offset + 1 (new objects star at last pixel)
        /// </summary>
        public Vector2 SizeInternal => PanelOverflowBehavior == PanelOverflowBehavior.VerticalScroll
            ? Size - (Padding * 2 + VerticalScrollbar.DefaultSize)
            : Size - Padding * 2;

        public PanelEmpty(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor: anchor, offset: offset)
        {
            Skin = PanelSkin.None;
        }
    }

    // padding works as offset + 1 (new objects star at last pixel)
    public class PanelBrownThick : PanelEmpty
    {
        public PanelBrownThick(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor, offset: offset)
        {
            Skin = PanelSkin.Fancy;
            Padding = new Vector2(7, 7);
        }
    }

    public class PanelBrownThin : PanelEmpty
    {
        public PanelBrownThin(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor, offset: offset)
        {
            Skin = PanelSkin.Default;
            Padding = new Vector2(3, 3);
        }
    }

    public class PanelBlackThin : PanelEmpty
    {
        public PanelBlackThin(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor, offset: offset)
        {
            Skin = PanelSkin.Simple;
            Padding = new Vector2(3,3);
        }
    }

    public class ButtonPanel : PanelEmpty
    {
        public PanelSkin SkinChecked { get; set; }
        public PanelSkin SkinUnchecked { get; set; }

        // button value when in toggle mode
        public bool Checked { get; set; }

        public ButtonPanel(Vector2 size, PanelSkin skinChecked = PanelSkin.Default, PanelSkin skinUnchecked = PanelSkin.Simple, Anchor anchor = Anchor.Auto, Vector2? offset = null) : base(size, anchor, offset)
        {
            SkinChecked = skinChecked;
            Skin = SkinUnchecked = skinUnchecked;
            Padding = new Vector2(3, 3);
        }

        public void Check()
        {
            Checked = !Checked;
            Skin = Checked ? SkinChecked : SkinUnchecked;
        }
    }
}