#region USING STATEMENTS

using System;
using Microsoft.Xna.Framework;

#endregion

namespace Game1
{
    /// <summary>
    /// Custom event argument which includes the index of the player who
    /// triggered the event. This is used by the MenuEntry.Selected event.
    /// </summary>
    class PlayerIndexEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            this.PlayerIndex = playerIndex;
        }


        /// <summary>
        /// Gets the index of the player who triggered this event.
        /// </summary>
        public PlayerIndex PlayerIndex { get; }
    }
}
