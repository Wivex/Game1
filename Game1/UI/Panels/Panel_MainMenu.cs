using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;

namespace Game1.UI.Panels
{
    public static class Panel_MainMenu
    {
        public static void Init(Vector2 size, Game1 game)
        {
            // create panel and add to list of screenPanels and manager
            var mainMenuPanel = new PanelBrownThick(size)
            {
                Padding = new Vector2(30, 30)
            };
            // add to list of panels to b able to hide all others?
            game.ScreenPanels.Add(mainMenuPanel);

            mainMenuPanel.AddChild(new Header("Game Name?"));
            mainMenuPanel.AddChild(new HorizontalLine());
            mainMenuPanel.AddChild(new Button("New Game")
            {
                OnClick = entity => { Panel_Gameplay.Init(game); }
            });
            mainMenuPanel.AddChild(new Button("Load Game")
            {
                Disabled = true
            });
            mainMenuPanel.AddChild(new Button("Options")
            {
                Disabled = true
            });
            mainMenuPanel.AddChild(new Button("Exit") {OnClick = entity => game.Exit()});

            UserInterface.Active.AddEntity(mainMenuPanel);
        }
    }
}
