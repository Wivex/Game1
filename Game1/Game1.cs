using Microsoft.Xna.Framework;

namespace Game1
{
    public class Game1 : Game
    {
        ScreenManager screenManager;

        public Game1()
        {
            Content.RootDirectory = "Content";
            // initializes unnamed GraphicsDevice for the Game1 (just to work)
            new GraphicsDeviceManager(this);
            Services.AddService(typeof(IScreenFactory), new ScreenFactory());
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            AddInitialScreens();
        }

        public void AddInitialScreens()
        {
            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen());
            screenManager.AddScreen(new MainMenuScreen());
        }
    }
}
