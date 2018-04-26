using System.Collections.Generic;
using Game1.Objects.Units;
using Game1.UI.Panels;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Game
    {
        public Vector2 GameScreenResolution { get; set; } = new Vector2(1280, 720);
        public List<Panel> ScreenPanels { get; } = new List<Panel>();
        public SpriteBatch SpriteBatch { get; set; }
        
        /// <summary>
        /// Sets content root dir, default resolution and screen position
        /// </summary>
        public Game1()
        {
            Content.RootDirectory = "Content";
            var graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = (int)GameScreenResolution.X,
                PreferredBackBufferHeight = (int)GameScreenResolution.Y
            };

            var hero = new Hero();

            Window.Position =
                new Point(
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - graphics.PreferredBackBufferWidth / 2,
                    (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 -
                     graphics.PreferredBackBufferHeight / 2) / 2);
        }

        /// <summary>
        /// Invoked by Game.Run(), right after the Game() contructor
        /// </summary>
        protected override void Initialize()
        {
            // UI manager default initialization (don't bother, leave as it is)
            //UserInterface.Initialize(Content,BuiltinThemes.lowres);
            UserInterface.Initialize(Content, "custom");
            UserInterface.Active.UseRenderTarget = true;
            UserInterface.Active.IncludeCursorInRenderTarget = false;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            // main UI logic entry
            InitUI();
        }

        /// <summary>
        /// Create the top bar with next / prev buttons etc, and init all UI example screenPanels.
        /// </summary>        
        public void InitUI()
        {
            var background = new Image(Content.Load<Texture2D>("background temp"), GameScreenResolution, anchor: Anchor.Center);
            UserInterface.Active.Root.AddChild(background);

            Panel_MainMenu.Init(new Vector2(450, 410), this);
        }

        /// <summary>
        /// Called after we change current example index, to hide all examples
        /// except for the currently active example + disable prev / next buttons if
        /// needed (if first or last example).
        /// </summary>
        public void ChangeActivePanel(Panel activePanel)
        {
            foreach (var panel in ScreenPanels)
            {
                panel.Visible = false;
            }
            activePanel.Visible = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // make sure window is focused
            if (!IsActive)
                return;

            // exit on escape
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update UI
            UserInterface.Active.Update(gameTime);

            // call base update
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // UI draw and rendering (don't bother, leave as it is)
            UserInterface.Active.Draw(SpriteBatch);
            UserInterface.Active.DrawMainRenderTarget(SpriteBatch);
            base.Draw(gameTime);
        }
    }
}
