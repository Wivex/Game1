using System;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.UI
{
    /// <summary>
    /// Sugarcoat to implement SizeInternal property for GeonUI Panel class
    /// </summary>
    public class PanelEmpty : Panel
    {
        public virtual int PanelTextureBorderWidth { get; set; } = 0;

        /// <summary>
        /// Padding works as offset + 1 (new objects star at last pixel)
        /// </summary>
        public Vector2 SizeInternal => PanelOverflowBehavior == PanelOverflowBehavior.VerticalScroll
            ? Size - (Padding * 2 + VerticalScrollbar.DefaultSize)
            : Size - Padding * 2;

        public PanelEmpty(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor: anchor, offset: offset)
        {
            Skin = PanelSkin.None;
            // *2 for 2 pixels border draw
            Padding = new Vector2(PanelTextureBorderWidth * 2, PanelTextureBorderWidth * 2);
            BeforeDraw += e => { UpdateChildrenVisibility(); };
        }

        public void InitFloatingText(string text, TimeSpan lifeTime, Texture2D icon = null)
        {
            new FloatingText(text, this, lifeTime, icon);
        }

        // TODO: check if needed
        public virtual void UpdateChildrenVisibility()
        {
        }
    }

    // padding works as offset + 1 (new objects star at last pixel)
    public class PanelBrownExternal : PanelEmpty
    {
        public override int PanelTextureBorderWidth { get; set; } = 2;

        public PanelBrownExternal(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor, offset)
        {
            Skin = PanelSkin.Fancy;
        }
    }

    public class PanelBrownInternal : PanelEmpty
    {
        public override int PanelTextureBorderWidth { get; set; } = 2;

        public PanelBrownInternal(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor, offset)
        {
            Skin = PanelSkin.Default;
        }
    }

    public class PanelBlack : PanelEmpty
    {
        public override int PanelTextureBorderWidth { get; set; } = 2;

        public PanelBlack(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor, offset)
        {
            Skin = PanelSkin.Simple;
        }
    }

    public class PanelFramed : PanelEmpty
    {
        public override int PanelTextureBorderWidth { get; set; } = 5;

        public PanelFramed(Vector2 size, Anchor anchor = Anchor.Center, Vector2? offset = null) : base(size, anchor, offset)
        {
            Skin = PanelSkin.Golden;
        }
    }

    public class ButtonPanel : PanelBlack
    {
        public PanelSkin SkinChecked { get; set; }
        public PanelSkin SkinUnchecked { get; set; }

        // button value when in toggle mode
        public bool Checked { get; set; }

        public ButtonPanel(Vector2 size) : base(size, Anchor.Auto)
        {
            SkinChecked = PanelSkin.Default;
            SkinUnchecked = PanelSkin.Simple;
        }

        public void Check()
        {
            Checked = !Checked;
            Skin = Checked ? SkinChecked : SkinUnchecked;
        }
    }
}