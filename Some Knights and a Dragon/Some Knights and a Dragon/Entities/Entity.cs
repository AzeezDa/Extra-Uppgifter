
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities
{
    public enum TextureDirection { Left, Right, Up, Down } // Direction of the texture based on direction of the movement

    public abstract class Entity // Any object that moves and has a texture.
    {
        public Sprite Sprite { get; protected set; }
        public string Name { get; protected set; }

        public bool CollidingWithBoundries { get; private set; }

        // TO BE CHANGED FOR A MORE PHYSICALLY ACCURATE MODEL, THE POSITION, SPEED, ACCELRERATION MODEL WITHOUT DIRECTION
        public Vector2 Position { get; protected set; }
        public  Vector2 Speed { get; protected set; }
        public  Vector2 Velocity { get; protected set; }
        public  Vector2 Acceleration { get; protected set; }
        protected bool ObeysGravity = true; // If it is changed by gravity of the GameArea

        public Entity()
        {
            Acceleration = new Vector2(0, 0);
        }

        public Rectangle HitBox { get; protected set; }

        public TextureDirection TextureDirection { get; protected set; }

        protected virtual void LoadSprite(string filepath)
        {
            Sprite = new Sprite(filepath);
        }

        protected virtual void LoadSprite(string filepath, int width, int height)
        {
            Sprite = new Sprite(filepath, width, height, 12);
        }

        // Updates the entity
        public virtual void Update(ref GameTime gameTime)
        {
            CollidingWithBoundries = false;
            Sprite.Update(ref gameTime);


            // Checks direction of movement to change direction of texture
            if (Velocity.X < 0)
            {
                TextureDirection = TextureDirection.Left;
            }
            if (Velocity.X > 0)
            {
                TextureDirection = TextureDirection.Right;
            }

            // Updates the hitbox
            HitBox = new Rectangle(
                (int)Position.X - Sprite.Width / 2 * Sprite.Scale,
                (int)Position.Y - Sprite.Height / 2 * Sprite.Scale,
                Sprite.Width * Sprite.Scale,
                Sprite.Width * Sprite.Scale);

            // Updates movment, TO BE CHANGED FOR ACCURACY
            Acceleration = ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Gravity;
            Velocity += Acceleration * ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Gravity * (ObeysGravity ? 1 : 0);
            Position += Acceleration * (float)Math.Pow(gameTime.ElapsedGameTime.TotalSeconds, 2.0) / 2 + Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // CHECKS COLLISION WITH BOUNDRIES
            if (Position.X - HitBox.Width / 2 < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left)
            {
                Position = new Vector2(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left + HitBox.Width / 2, Position.Y);
                Velocity = Vector2.Zero;
                CollidingWithBoundries = true;
            }
            if (Position.X + HitBox.Width / 2 > ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Right)
            {
                Position = new Vector2(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Right - HitBox.Width / 2, Position.Y);
                Velocity = Vector2.Zero;
                CollidingWithBoundries = true;
            }
            if (Position.Y - HitBox.Height  / 2 < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left)
            {
                Position = new Vector2(Position.X, ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Top + HitBox.Height / 2);
                Velocity = Vector2.Zero;
                CollidingWithBoundries = true;
            }
            if (Position.Y + HitBox.Height / 2> ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Bottom)
            {
                Position = new Vector2(Position.X, ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Bottom - HitBox.Height / 2);
                Velocity = Vector2.Zero;
                CollidingWithBoundries = true;
            }
        }

        // Draws the sprite
        public virtual void Draw(ref SpriteBatch _spriteBatch)
        {
            Sprite.Draw(ref _spriteBatch, Position, TextureDirection);
        }

        public void ChangePosition(Vector2 position)
        {
            Position = position;
        }
        public void ChangeVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void ChangeAcceleration(Vector2 acceleration)
        {
            Acceleration = acceleration;
        }

        public void AddToPosition(Vector2 position)
        {
            Position += position;
        }
        public void AddToVelocity(Vector2 velocity)
        {
            Velocity += velocity;
        }

        public void AddToAcceleration(Vector2 acceleration)
        {
            Acceleration += acceleration;
        }

        public void MultiplyWithPosition(Vector2 position)
        {
            Position *= position;
        }
        public void MultiplyWithVelocity(Vector2 velocity)
        {
            Velocity *= velocity;
        }

        public void MultiplyWithAcceleration(Vector2 acceleration)
        {
            Acceleration *= acceleration;
        }
    }
}
