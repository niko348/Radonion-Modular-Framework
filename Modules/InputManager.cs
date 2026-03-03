using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Dirge.Systems
{
    /// <summary>
    /// A static global system to manage Keyboard and Mouse states.
    /// Call InputManager.Update() at the start of your main game update loop.
    /// </summary>
    public class InputManager
    {

        #region Fields

        // Mouse
        private static MouseState _currentMouseState;
        private static MouseState _prevMouseState;

        // Keyboard
        private static KeyboardState _currentKeyState;
        private static KeyboardState _prevKeyState;

        #endregion

        #region Properties

        /// <summary> The current X/Y position of the mouse cursor. </summary>
        public static Point MousePosition => _currentMouseState.Position;

        #endregion

        /// <summary> Synchronizes input states. Must be called once per frame. </summary>
        public static void Update()
        {
            _prevMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            _prevKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
        }

        #region Mouse Logic

        /// <summary> Returns true only on the frame the Left Mouse Button is pressed. </summary>
        public static bool IsLeftMousePressed()
            => _currentMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released;

        /// <summary> Returns true as long as the Left Mouse Button is held down. </summary>
        public static bool IsLeftMouseHeld()
            => _currentMouseState.LeftButton == ButtonState.Pressed;


        /// <summary> Returns true only on the frame the Left Mouse Button is released. </summary>
        public static bool IsLeftMouseReleased()
            => _currentMouseState.LeftButton == ButtonState.Released && _prevMouseState.LeftButton == ButtonState.Pressed;


        /// <summary> Returns true only on the frame the Left Mouse Button is pressed. </summary>
        public static bool IsRightMousePressed()
            => _currentMouseState.RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released;

        /// <summary> Returns true as long as the Left Mouse Button is held down. </summary>
        public static bool IsRightMouseHeld()
            => _currentMouseState.RightButton == ButtonState.Pressed;


        /// <summary> Returns true only on the frame the Left Mouse Button is released. </summary>
        public static bool IsRightMouseReleased()
            => _currentMouseState.RightButton == ButtonState.Released && _prevMouseState.RightButton == ButtonState.Pressed;


        /// <summary> Returns true only on the frame the Middle Mouse Button (scroll wheel) is pressed. </summary>
        public static bool IsMiddleMousePressed()
            => _currentMouseState.MiddleButton == ButtonState.Pressed && _prevMouseState.MiddleButton == ButtonState.Released;

        /// <summary> Returns true as long as the Middle Mouse Button is held down. </summary>
        public static bool IsMiddleMouseHeld()
            => _currentMouseState.MiddleButton == ButtonState.Pressed;

        /// <summary> Returns true only on the frame the Middle Mouse Button is released. </summary>
        public static bool IsMiddleMouseReleased()
            => _currentMouseState.MiddleButton == ButtonState.Released && _prevMouseState.MiddleButton == ButtonState.Pressed;


        /// <summary> Calculates the movement of the scroll wheel since the last frame. </summary>
        /// <returns> Positive for Up, Negative for Down, 0 for no movement. </returns>
        public static int GetScrollDelta()
            => _currentMouseState.ScrollWheelValue - _prevMouseState.ScrollWheelValue;

        #endregion

        #region Keyboard Logic

        /// <summary> Returns true only on the frame the key is first pressed. </summary>
        public static bool IsKeyPressed(Keys key)
            => _currentKeyState.IsKeyDown(key) && _prevKeyState.IsKeyUp(key);

        /// <summary> Returns true as long as the key is being held down. </summary>
        public static bool IsKeyHeld(Keys key)
            => _currentKeyState.IsKeyDown(key);

        /// <summary> Returns true only on the frame the key is released. </summary>
        public static bool IsKeyReleased(Keys key)
            => _currentKeyState.IsKeyUp(key) && _prevKeyState.IsKeyDown(key);

        #endregion

        #region Advanced Helpers

        /// <summary>
        /// Returns a Normalized Vector2 for Top-Down movement.
        /// Prevents "diagonal speed boost" by ensuring the vector length is 1.
        /// </summary>
        public static Vector2 GetTopDownMovement()
        {
            Vector2 direction = Vector2.Zero;

            if (IsKeyHeld(Keys.W)) direction.Y -= 1;
            if (IsKeyHeld(Keys.S)) direction.Y += 1;
            if (IsKeyHeld(Keys.A)) direction.X -= 1;
            if (IsKeyHeld(Keys.D)) direction.X += 1;

            if (direction != Vector2.Zero) direction.Normalize();

            return direction;
        }

        /// <summary>
        /// Returns a Vector2 for Side-Scrollers. 
        /// X is movement, Y is for looking up/down or ladders.
        /// </summary>
        public static Vector2 GetSideScrollingMovement()
        {
            Vector2 direction = Vector2.Zero;

            if (IsKeyHeld(Keys.A)) direction.X -= 1;
            if (IsKeyHeld(Keys.D)) direction.X += 1;

            return direction;
        }

        /// <summary>
        /// Checks if the mouse cursor is currently within a specific rectangle.
        /// </summary>
        public static bool IsMouseInBounds(Rectangle bounds)
        {
            return bounds.Contains(MousePosition);
        }

        #endregion

    }
}
