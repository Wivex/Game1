using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    /// <summary>
    /// Helper class represents a single entry in a MenuScreen. By default this
    /// just draws the entry text string, but it can be customized to display menu
    /// entries in different ways. This also provides an event that will be raised
    /// when the menu entry is selected.
    /// </summary>
    class MenuEntry
    {
        #region FIELDS
        /// <summary>
        /// Tracks a fading selection effect on the entry.
        /// </summary>
        /// <remarks>
        /// The entries transition out of the selection effect when they are deselected.
        /// </remarks>
        float selectionFade;
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Gets or sets the text of this menu entry.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the position at which to draw this menu entry.
        /// </summary>
        public Vector2 Position { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Event raised when the menu entry is selected.
        /// </summary>
        public event EventHandler Selected;

        /// <summary>
        /// Method for raising the Selected event.
        /// </summary>
        protected internal virtual void OnSelectEntry()
        {
            Selected?.Invoke(this, new EventArgs());
        }
        #endregion

        #region INITIALIZATION


        /// <summary>
        /// Constructs a new menu entry with the specified text.
        /// </summary>
        public MenuEntry(string text)
        {
            Text = text;
        }


        #endregion

        #region UPDATE & DRAW


        /// <summary>
        /// Updates the menu entry.
        /// </summary>
        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // When the menu selection changes, entries gradually fade between
            // their selected and deselected appearance, rather than instantly
            // popping to the new state.
            var fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
        }


        /// <summary>
        /// Draws the menu entry. This can be overridden to customize the appearance.
        /// </summary>
        public virtual void Draw(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
            // Draw the selected entry in yellow, otherwise white.
            var color = isSelected ? Color.Yellow : Color.White;

            // Pulsate the size of the selected menu entry.
            var time = gameTime.TotalGameTime.TotalSeconds;
            
            var pulsate = (float)Math.Sin(time * 6) + 1;

            var scale = 1 + pulsate * 0.05f * selectionFade;

            // Modify the alpha to fade text out during transitions.
            color *= screen.TransitionAlpha;

            // Draw text, centered on the middle of each line.
            var screenManager = screen.ScreenManager;
            var spriteBatch = screenManager.SpriteBatch;
            var font = screenManager.Font;

            var origin = new Vector2(0, font.LineSpacing / 2);

            spriteBatch.DrawString(font, Text, Position, color, 0,
                                   origin, scale, SpriteEffects.None, 0);
        }


        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }


        /// <summary>
        /// Queries how wide the entry is, used for centering on the screen.
        /// </summary>
        public virtual int GetWidth(MenuScreen screen)
        {
            return (int)screen.ScreenManager.Font.MeasureString(Text).X;
        }


        #endregion
    }
}
