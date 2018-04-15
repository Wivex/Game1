using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This class tracks both the current and previous state of the input device, and implements 
    /// query methods for high level input actions such as "move up through the menu"
    /// or "pause the game".
    /// </summary>
    public class InputState
    {
        public KeyboardState CurrentKeyboardState = new KeyboardState();
        public KeyboardState LastKeyboardState = new KeyboardState();

        /// <summary>
        /// Helper for checking if a key was pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsKeyPressed(Keys key) => CurrentKeyboardState.IsKeyDown(key);

        /// <summary>
        /// Helper for checking if a key was newly pressed during this update. The
        /// controllingPlayer parameter specifies which player to read input for.
        /// If this is null, it will accept input from any player. When a keypress
        /// is detected, the output playerIndex reports which player pressed it.
        /// </summary>
        public bool IsNewKeyPress(Keys key) => CurrentKeyboardState.IsKeyDown(key) &&
                                               LastKeyboardState.IsKeyUp(key);

        /// <summary>
        /// Reads the latest state user input.
        /// </summary>
        public void Update()
        {
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }
    }
}
