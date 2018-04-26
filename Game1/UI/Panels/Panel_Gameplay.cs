using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class Panel_Gameplay
    {
        public static void Init(Game1 game)
        {
            // initialize gameplay screen panel
            var gameplayPanel = new Panel(game.GameScreenResolution, PanelSkin.None);
            game.ScreenPanels.Add(gameplayPanel);
            UserInterface.Active.AddEntity(gameplayPanel);
            game.ChangeActivePanel(gameplayPanel);

            // initialize panel tabs with buttons
            var buttonsPanel =
                new PanelBrownThin(new Vector2(gameplayPanel.Size.X, Button.DefaultSize.Y), Anchor.TopCenter);
            var tabsPanel =
                new PanelBrownThin(new Vector2(gameplayPanel.Size.X, gameplayPanel.Size.Y - buttonsPanel.Size.Y),
                    Anchor.BottomCenter);
            var buttonTabs = new PanelTabs_Button(buttonsPanel, tabsPanel);
            gameplayPanel.AddChild(buttonTabs);

            // initialize buttons for panel tabs
            var militaryTabButton = new Button("Military", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y));
            var button2 = new Button("RESERVED", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y))
            {
                Disabled = true
            };
            var button3 = new Button("RESERVED", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y))
            {
                Disabled = true
            };
            var menuButton = new Button("Menu", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y))
            {
                Disabled = true
            };
            
            // initialize panels for panel tabs
            var militaryPanel = new PanelEmpty(tabsPanel.SizeInternal);
            var panel2 = new PanelEmpty(tabsPanel.SizeInternal);
            var panel3 = new PanelEmpty(tabsPanel.SizeInternal);
            var menuPanel = new PanelEmpty(tabsPanel.SizeInternal);

            // initialize tabs for panel tabs
            var militaryTab = buttonTabs.AddTab(militaryTabButton, militaryPanel);
            var tab2 = buttonTabs.AddTab(button2, panel2);
            var tab3 = buttonTabs.AddTab(button3, panel3);
            var menuTab = buttonTabs.AddTab(menuButton, menuPanel);

            // initialize military tab panel
            Tab_Military.Init(game, militaryTab.TabPanel);
        }
    }
}