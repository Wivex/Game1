using Game1.UI.GeonUI_Overrides;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class PanelGameplay
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
            var buttonTabs = new ButtonTabs(buttonsPanel, tabsPanel);
            gameplayPanel.AddChild(buttonTabs);

            // initialize buttons for panel tabs
            var expeditionsButton = new ButtonNew("Expeditions", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y));
            var button2 = new ButtonNew("RESERVED", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y))
            {
                Disabled = true
            };
            var debugButton = new ButtonNew("Debug", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y));
            var menuButton = new ButtonNew("Menu", anchor: Anchor.AutoInline,
                size: new Vector2((int) buttonsPanel.SizeInternal.X / 4, buttonsPanel.SizeInternal.Y))
            {
                Disabled = true
            };

            // initialize panels for panel tabs
            var expeditionsPanel = new PanelEmpty(tabsPanel.SizeInternal);
            var panel2 = new PanelEmpty(tabsPanel.SizeInternal);
            var debugPanel = new PanelEmpty(tabsPanel.SizeInternal);
            var menuPanel = new PanelEmpty(tabsPanel.SizeInternal);

            // initialize tabs for panel tabs
            var expeditionsTab = buttonTabs.AddTab(expeditionsButton, expeditionsPanel);
            var tab2 = buttonTabs.AddTab(button2, panel2);
            var debugTab = buttonTabs.AddTab(debugButton, debugPanel);
            var menuTab = buttonTabs.AddTab(menuButton, menuPanel);

            var ExpeditionsTab = new TabExpeditions(expeditionsPanel.SizeInternal);
            expeditionsPanel.AddChild(ExpeditionsTab);

            TabDebug.Init(debugPanel);

            // click expedition button by default
            expeditionsButton.DoClick();
        }
    }
}