using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    /// <summary>
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        #region FIELDS
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> tempScreensList = new List<GameScreen>();
        bool isInitialized;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font { get; set; }

        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled => true;

        /// <summary>
        /// Gets a blank texture that can be used by the screens.
        /// </summary>
        public Texture2D BlankTexture { get; set; }
        #endregion

        #region INITIALIZATION
        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(Game game) : base(game)
        {
        }

        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            isInitialized = true;
        }
        
        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            var content = Game.Content;

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = content.Load<SpriteFont>("menufont");
            BlankTexture = content.Load<Texture2D>("blank");

            // Tell each of the screens to load their content.
            foreach (var screen in screens)
            {
                screen.Activate();
            }
        }

        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (var screen in screens)
            {
                screen.Unload();
            }
        }
        #endregion

        #region UPDATE & DRAW
        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Read the keyboard

            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            tempScreensList.Clear();

            foreach (var screen in screens)
                tempScreensList.Add(screen);

            var otherScreenHasFocus = !Game.IsActive;
            var coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (tempScreensList.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                var screen = tempScreensList[tempScreensList.Count - 1];

                tempScreensList.RemoveAt(tempScreensList.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
            if (TraceEnabled)
                TraceScreens();
        }

        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            var screenNames = new List<string>();

            foreach (var screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }

        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            foreach (var screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw();
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen)
        {
            screen.ScreenManager = this;
            screen.IsClosing = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.Activate();
            }

            screens.Add(screen);
        }

        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.Unload();
            }

            screens.Remove(screen);
            tempScreensList.Remove(screen);
        }

        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(BlankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            SpriteBatch.End();
        }
        #endregion
    }
}