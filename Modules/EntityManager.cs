using Microsoft.Xna.Framework;

namespace /* Insert your directory here. Example: Game.Modules */
{
    public class Entity
    {
        // Core Physics Variables
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        // Constants for "Game Feel"
        public float Friction = 0.90f;
        public float Gravity = 15.0f;
        public float MaxSpeed = 500.0f;

        public virtual void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Acceleration.Y += Gravity;

            Velocity += Acceleration * deltaTime;

            Velocity *= Friction;

            Velocity.X = MathHelper.Clamp(Velocity.X, -MaxSpeed, MaxSpeed);

            Position += Velocity * deltaTime;

            Acceleration = Vector2.Zero;
        }
    }
}

