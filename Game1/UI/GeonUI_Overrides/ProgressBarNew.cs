using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.GeonUI_Overrides
{
    public class ProgressBarNew : ProgressBar
    {
        public ProgressBarNew(int min, int max, Vector2 size, Anchor anchor = Anchor.Auto, Vector2? offset = null) : base((uint)min, (uint)max, size, anchor, offset)
        {
        }

        protected override void DoOnMouseWheelScroll() { }
        protected override void DoWhileMouseDown() { }
    }
}