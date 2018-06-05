using System.Collections.Generic;
using Game1.Concepts;
using Game1.Engine;
using Game1.UI;
using Game1.UI.GeonUI_Overrides;
using Game1.UI.Panels;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Game1 : Game
    {
        public Vector2 GameScreenResolution { get; set; } = new Vector2(1280, 720);
        public List<Panel> ScreenPanels { get; } = new List<Panel>();
        public SpriteBatch SpriteBatch { get; set; }
        public bool GameplayRunning { get; set; }

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
            // set current game session as global reference
            Globals.Init(this);
            DB.Init();

            // generate xml template based on pre initialized data class
            //new XMLData.AbilityData().GenerateTemplate($@"{Content.RootDirectory}\Template.xml");

            // Loads GeonUI textures and sets defaults
            UserInterface.Initialize(Content, "custom");
            UserInterface.Active.UseRenderTarget = true;
            UserInterface.Active.GenerateTooltipFunc = NewGenerateTooltipFunc;
            UserInterface.TimeToShowTooltipText = 0.5f;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            // main UI entry
            InitUI();
        }

        public Entity NewGenerateTooltipFunc(Entity source)
        {
            // no tooltip text? return null
            if (source.ToolTipText == null) return null;

            // create tooltip paragraph
            var tooltip = new Paragraph(source.ToolTipText, size: new Vector2(500, -1)) {BackgroundColor = Color.Black};

            // add callback to update tooltip position
            tooltip.BeforeDraw += e =>
            {
                var position = UserInterface.Active.GetTransformedCursorPos(new Vector2(45, -25));

                // update tooltip position
                tooltip.SetPosition(Anchor.TopLeft, position / UserInterface.Active.GlobalScale);
            };

            // return tooltip object
            return tooltip;
        }

        /// <summary>
        /// Create the top bar with next / prev buttons etc, and init all UI example screenPanels.
        /// </summary>        
        public void InitUI()
        {
            var background = new ImageNew(Globals.TryLoadTexture($@"{Globals.DataPathBase}\Background"), GameScreenResolution);
            UserInterface.Active.Root.AddChild(background);

            PanelMainMenu.Init(new Vector2(450, 410), this);
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
            // update global reference
            Globals.GameTime = gameTime;

            // update all gameplay tabs, according to game speed
            //  && gameTime.TotalGameTime.TotalMilliseconds % Globals.GameTick.TotalMilliseconds < gameTime.ElapsedGameTime.TotalMilliseconds
            if (GameplayRunning)
                UpdateTabs();

            // update inputs, root panel, tooltips, select active panel
            UserInterface.Active.Update(gameTime);

            base.Update(gameTime);
        }

        public void UpdateTabs()
        {
            Globals.TabExpeditions.UpdateData();
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

            FloatingText.DrawAllTexts();

            base.Draw(gameTime);
        }
    }
}
