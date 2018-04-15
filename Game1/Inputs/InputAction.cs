using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// Defines an action that is designated by some set of buttons and/or keys.
    /// 
    /// The way actions work is that you define a set of buttons and keys that trigger the action. You can
    /// then evaluate the action against an InputState which will test to see if any of the buttons or keys
    /// are pressed by a player. You can also set a flag that indicates if the action only occurs once when
    /// the buttons/keys are first pressed or whether the action should occur each frame.
    /// 
    /// Using this InputAction class means that you can configure new actions based on keys and buttons
    /// without having to directly modify the InputState type. This means more customization by your games
    /// without having to change the core classes of Game State Management.
    /// </summary>
    public class InputAction
    {
        Keys[] keys;
        bool newPressOnly;
        
        delegate bool KeyPress(Keys key);

        /// <summary>
        /// Initializes a new InputAction.
        /// </summary>
        /// <param name="buttons">An array of buttons that can trigger the action.</param>
        /// <param name="keys">An array of keys that can trigger the action.</param>
        /// <param name="newPressOnly">Whether the action only occurs on the first press of one of the buttons/keys, 
        /// false if it occurs each frame one of the buttons/keys is down.</param>
        public InputAction(Buttons[] buttons, Keys[] keys, bool newPressOnly)
        {
            // Store the keys. If the arrays are null, we create a 0 length array so we don't
            // have to do null checks in the Evaluate method
            this.keys = keys != null ? keys.Clone() as Keys[] : new Keys[0];
            this.newPressOnly = newPressOnly;
        }

        /// <summary>
        /// Evaluates the action against a given InputState.
        /// </summary>
        /// <param name="state">The InputState to test for the action.</param>
        /// <param name="controllingPlayer">The player to test, or null to allow any player.</param>
        /// <param name="player">If controllingPlayer is null, this is the player that performed the action.</param>
        /// <returns>True if the action occurred, false otherwise.</returns>
        public bool Evaluate(InputState state)
        {
            // Figure out which delegate methods to map from the state which takes care of our "newPressOnly" logic
            KeyPress keyTest;
            if (newPressOnly)
            {
                keyTest = state.IsNewKeyPress;
            }
            else
            {
                keyTest = state.IsKeyPressed;
            }

            // Now we simply need to invoke the appropriate methods for each key in our collections
            foreach (var key in keys)
            {
                if (keyTest(key))
                    return true;
            }

            return false;
        }
    }
}
