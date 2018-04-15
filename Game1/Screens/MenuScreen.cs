#region File Description
//-----------------------------------------------------------------------------
// MenuScreen.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region USING STATEMENTS

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace Game1
{
    /// <summary>
    /// Base class for screens that contain a menu of options. The user can
    /// move up and down to select an entry, or cancel to back out of the screen.
    /// </summary>
    abstract class MenuScreen : GameScreen
    {
        #region FIELDS

        List<MenuEntry> menuEntries = new List<MenuEntry>();
        int selectedEntry;
        string menuTitle;

        InputAction menuUp;
        InputAction menuDown;
        InputAction menuSelect;
        InputAction menuCancel;

        #endregion

        #region PROPERTIES


        /// <summary>
        /// Gets the list of menu entries, so derived classes can add
        /// or change the menu contents.
        /// </summary>
        protected IList<MenuEntry> MenuEntries => menuEntries;

        #endregion

        #region INITIALIZATION


        /// <summary>
        /// Constructor.
        /// </summary>
        public MenuScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            menuUp = new InputAction(
                new[] { Buttons.DPadUp, Buttons.LeftThumbstickUp }, 
                new[] { Keys.Up },
                true);
            menuDown = new InputAction(
                new[] { Buttons.DPadDown, Buttons.LeftThumbstickDown },
                new[] { Keys.Down },
                true);
            menuSelect = new InputAction(
                new[] { Buttons.A, Buttons.Start },
                new[] { Keys.Enter, Keys.Space },
                true);
            menuCancel = new InputAction(
                new[] { Buttons.B, Buttons.Back },
                new[] { Keys.Escape },
                true);
        }


        #endregion

        #region HANDLE INPUT


        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            // Move to the previous menu entry?
            if (menuUp.Evaluate(input))
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (menuDown.Evaluate(input))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            if (menuSelect.Evaluate(input))
            {
                OnSelectEntry(selectedEntry);
            }
            else if (menuCancel.Evaluate(input))
            {
                OnCancel();
            }
        }


        /// <summary>
        /// Handler for when the user has chosen a menu entry.
        /// </summary>
        protected virtual void OnSelectEntry(int entryIndex)
        {
            menuEntries[entryIndex].OnSelectEntry();
        }


        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel()
        {
            ExitScreen();
        }


        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, EventArgs e)
        {
            OnCancel();
        }


        #endregion

        #region UPDATE & DRAW


        /// <summary>
        /// Allows the screen the chance to position the menu entries. By default
        /// all menu entries are lined up in a vertical list, centered on the screen.
        /// </summary>
        protected virtual void UpdateMenuEntryLocations()
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            var position = new Vector2(0f, 175f);

            // update each menu entry's location in turn
            for (var i = 0; i < menuEntries.Count; i++)
            {
                var menuEntry = menuEntries[i];
                
                // each entry is to be centered horizontally
                position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;

                if (ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                // set the entry's position
                menuEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += menuEntry.GetHeight(this);
            }
        }


        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (var i = 0; i < menuEntries.Count; i++)
            {
                var isSelected = IsActive && i == selectedEntry;

                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }


        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // make sure our entries are in the right place before we draw them
            UpdateMenuEntryLocations();

            var graphics = ScreenManager.GraphicsDevice;
            var spriteBatch = ScreenManager.SpriteBatch;
            var font = ScreenManager.Font;

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (var i = 0; i < menuEntries.Count; i++)
            {
                var menuEntry = menuEntries[i];

                var isSelected = IsActive && i == selectedEntry;

                menuEntry.Draw(this, isSelected, gameTime);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            var titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            var titleOrigin = font.MeasureString(menuTitle) / 2;
            var titleColor = new Color(192, 192, 192) * TransitionAlpha;
            var titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }


        #endregion
    }
}
