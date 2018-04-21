using System.Collections.Generic;
using System.Linq;
using Game1.Objects;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class Panel_Gameplay
    {
        public static void Init(Game1 game)
        {
            var gameplayPanel = new Panel(game.GameScreenResolution, PanelSkin.Simple);
            var selectionTabs = new PanelTabs();
            selectionTabs.BackgroundSkin = PanelSkin.Default;
            gameplayPanel.AddChild(selectionTabs);
            InitTabs(game,selectionTabs);
            game.ScreenPanels.Add(gameplayPanel);
            UserInterface.Active.AddEntity(gameplayPanel);
            game.ChangeActivePanel(gameplayPanel);
        }

        private static void InitTabs(Game1 game, PanelTabs tabs)
        {
            var borderSize = Constants.panelBorderWidth;

            var militaryTab = tabs.AddTab("Military");
            militaryTab.panel.Padding = new Vector2(borderSize, borderSize);

            var panelSize = new Vector2(
                (int) (game.GameScreenResolution.X - borderSize * 2) / 3,
                game.GameScreenResolution.Y - (borderSize * 2 + militaryTab.button.Size.Y));
            // TODO: add vertical separator
            var unitsListPanel = new Panel(panelSize, anchor: Anchor.CenterLeft,offset:new Vector2(-2,0))
            {
                Skin = PanelSkin.None,
                PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll
            };
            var objectDetailsPanel = new Panel(panelSize, anchor: Anchor.CenterRight)
            {
                Skin = PanelSkin.Simple
            };
            militaryTab.panel.AddChild(objectDetailsPanel);
            militaryTab.panel.AddChild(unitsListPanel);
            var unitDetailsPanel = new Panel(panelSize,anchor:Anchor.Center,offset:new Vector2(3,0))
            {
                Skin = PanelSkin.Simple
            };
            militaryTab.panel.AddChild(unitDetailsPanel);

            var unitList = new List<Unit>();
            unitList.Add(new Unit());
            unitList.Add(new Unit());
            unitList.Add(new Unit());
            unitList.Add(new Unit());
            unitList.Add(new Unit());

            Panel_UnitsList.Init(game,unitsListPanel,unitList);



            {
                var tab = tabs.AddTab("RESERVED");
                tab.button.Disabled = true;
            }
            {
                var tab = tabs.AddTab("RESERVED");
                tab.button.Disabled = true;
            }
            {
                var tab = tabs.AddTab("RESERVED");
                tab.button.Disabled = true;
            }
            {
                var tab = tabs.AddTab("RESERVED");
                tab.button.Disabled = true;
            }
            {
                var tab = tabs.AddTab("RESERVED");
                tab.button.Disabled = true;
            }
        }
    }
}