using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.GeonUI_Overrides
{
    public class ButtonNew : Button
    {
        public ButtonNew(string text, ButtonSkin skin = ButtonSkin.Default, Anchor anchor = Anchor.Auto, Vector2? size = null, Vector2? offset = null) : base(text, skin, anchor, size, offset)
        {
        }

        public void DoClick()
        {
            DoOnClick();
        }
    }
}
