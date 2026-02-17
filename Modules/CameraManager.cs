using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace /* Insert your directory here. Example: Game.Modules */
{
    public class CameraManager
    {
        #region Fields

        private static float _zoom = 1f;

        #endregion

        #region Properties

        public static Vector2 Position { get; set; }
        public static float Rotation { get; set; }

        // Toggle this to switch between instant snapping and smooth gliding
        public static bool UseSmoothFollow { get; set; } = false;
        // How "heavy" the camera feels. 1.0 is instant, 0.05 is very floaty.
        public static float FollowLerp { get; set; } = 0.2f;

        public static float Zoom
        {
            get => _zoom;
            set => _zoom = MathHelper.Clamp(value, 0.1f, 10f);
        }

        /// <summary>
        /// The transformation matrix to be passed into SpriteBatch.Begin.
        /// </summary>
        public static Matrix Transform { get; private set; }

        #endregion

        /// <summary>
        /// Updates the transformation matrix based on current Position, Zoom, and Rotation.
        /// Call this in your Update loop after your player/target has moved.
        /// </summary>
        /// <param name="target">The world position to center the camera on.</param>
        /// <param name="viewport">The current game viewport (for screen centering).</param>
        public static void Update(Vector2 target, Viewport viewport)
        {
            if (UseSmoothFollow)
            {
                Position = Vector2.Lerp(Position, target, FollowLerp);
            }
            else
            {
                Position = target;
            }

            Vector2 origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);

            Transform = Matrix.CreateTranslation(new Vector3(-Position, 0)) *
                        Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(_zoom, _zoom, 1) *
                        Matrix.CreateTranslation(new Vector3(origin, 0));

        }

        /// <summary>
        /// Translates a screen coordinate (like Mouse Position) into a world coordinate.
        /// Essential for interacting with objects in the world when the camera has moved.
        /// </summary>
        public static Vector2 ScreenToWorld(Vector2 screenPos)
        {
            return Vector2.Transform(screenPos, Matrix.Invert(Transform));
        }
    }
}
