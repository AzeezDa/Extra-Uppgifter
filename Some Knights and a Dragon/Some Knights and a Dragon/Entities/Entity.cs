using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities
{
    public enum TextureDirection { Left, Right } // Direction of the texture based on direction of the movement

    public abstract class Entity // Any object that moves and has a texture.
    {
        public Sprite Sprite { get; protected set; }
        public string Name { get; protected set; }

        public bool CollidingWithBoundries { get; private set; }
        
        // TO BE CHANGED FOR A MORE PHYSICALLY ACCURATE MODEL, THE POSITION, SPEED, ACCELRERATION MODEL WITHOUT DIRECTION
        public Vector2 Position { get; protected set; }
        protected Vector2 Speed;
        protected Vector2 Direction;
        protected Vector2 Acceleration;

        public Entity()
        {
            Acceleration = new Vector2(0, 0);
        }

        public Rectangle HitBox { get; protected set; }

        // Hitbox of the entity, used for collision
        protected int HitBoxWidth = 0;
        protected int HitBoxHeight = 0;

        protected TextureDirection TextureDirection;

        // Updates the entity
        public virtual void Update(ref GameTime gameTime)
        {
            CollidingWithBoundries = false;
            Sprite.Update(ref gameTime);
            
            // Updates the hitbox
            HitBox = new Rectangle(
                (int)Position.X - HitBoxWidth * Sprite.Scale / 2,
                (int)Position.Y + (Sprite.Height - 2 * HitBoxHeight) * Sprite.Scale / 2,
                HitBoxWidth * Sprite.Scale,
                HitBoxHeight * Sprite.Scale);

            // Updates movment, TO BE CHANGED FOR ACCURACY
            Acceleration += ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds + Acceleration;

            // CHECKS COLLISION WITH BOUNDRIES
            if (Position.X - HitBox.Width / 2 < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left)
            {
                Position = new Vector2(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left + HitBox.Width / 2, Position.Y);
                Acceleration = Vector2.Zero;
                CollidingWithBoundries = true;
            }
            if (Position.X + HitBox.Width / 2 > ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Right)
            {
                Position = new Vector2(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Right - HitBox.Width / 2, Position.Y);
                Acceleration = Vector2.Zero;
                CollidingWithBoundries = true;
            }
            if (Position.Y - HitBox.Height  / 2 < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left)
            {
                Position = new Vector2(Position.X, ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Top + HitBox.Height / 2);
                Acceleration = Vector2.Zero;
                CollidingWithBoundries = true;
            }
            if (Position.Y + HitBox.Height / 2> ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Bottom)
            {
                Position = new Vector2(Position.X, ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Bottom - HitBox.Height / 2);
                Acceleration = Vector2.Zero;
                CollidingWithBoundries = true;
            }

            // Checks direction of movement to change direction of texture
            if (Direction.X < 0)
            {
                TextureDirection = TextureDirection.Left;
            }
            if (Direction.X > 0)
            {
                TextureDirection = TextureDirection.Right;
            }
        }

        // Draws the sprite
        public virtual void Draw(ref SpriteBatch _spriteBatch)
        {
            Sprite.Draw(ref _spriteBatch, Position, TextureDirection);
        }
    }
}
