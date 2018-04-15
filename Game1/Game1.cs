using Microsoft.Xna.Framework;

namespace Game1
{
    public class Game1 : Game
    {
        public Game1()
        {
            // initializes unnamed GraphicsDevice for the Game1 (just to work)
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            var screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            IsMouseVisible = true;

            // Activate the first screens.
            screenManager.AddScreen(new BackgroundScreen());
            //screenManager.AddScreen(new MainMenuScreen());
        }
    }
}
