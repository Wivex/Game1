using System;
using System.Collections.Generic;
using Game1.Concepts;
using Game1.Objects.Units;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class TabDebug
    {
        public static void Init(PanelEmpty parentPanel)
        {
            var debugButtonSize = new Vector2(200, 50);
            parentPanel.AddChild(new Button("New Expedition",size: debugButtonSize)
            {
                OnClick = entity =>
                {
                    var hero = new Hero("John", "Warrior");
                    new Expedition(hero, "Forest");
                }
            });
        }
    }
}