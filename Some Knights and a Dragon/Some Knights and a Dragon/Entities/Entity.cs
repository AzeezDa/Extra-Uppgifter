using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities
{
    public enum TextureDirection { Left, Right }

    public abstract class Entity
    {
        public Sprite Sprite { get; protected set; }
        public string Name { get; protected set; }
        public Vector2 Position { get; protected set; }
        protected Vector2 Speed;
        protected Vector2 Direction;
        protected Vector2 Acceleration;

        public Entity()
        {
            Acceleration = new Vector2(0, 0);
        }

        public Rectangle HitBox { get; protected set; }

        protected int HitBoxWidth = 0;
        protected int HitBoxHeight = 0;

        protected TextureDirection TextureDirection;

        public virtual void Update(ref GameTime gameTime)
        {
            Sprite.Update(ref gameTime);
            HitBox = new Rectangle(
                (int)Position.X - HitBoxWidth * Sprite.Scale / 2,
                (int)Position.Y + (Sprite.Height - 2 * HitBoxHeight) * Sprite.Scale / 2,
                HitBoxWidth * Sprite.Scale,
                HitBoxHeight * Sprite.Scale);

            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds + Acceleration;
            Acceleration += ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.X - HitBox.Width / 2 < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left)
            {
                Position = new Vector2(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left + HitBox.Width / 2, Position.Y);
                Acceleration = Vector2.Zero;
            }
            if (Position.X + HitBox.Width / 2 > ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Right)
            {
                Position = new Vector2(((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Right - HitBox.Width / 2, Position.Y);
                Acceleration = Vector2.Zero;
            }
            if (Position.Y - HitBox.Height  / 2 < ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Left)
            {
                Position = new Vector2(Position.X, ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Top + HitBox.Height / 2);
                Acceleration = Vector2.Zero;
            }
            if (Position.Y + HitBox.Height / 2> ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Bottom)
            {
                Position = new Vector2(Position.X, ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Bottom - HitBox.Height / 2);
                Acceleration = Vector2.Zero;
            }


            if (Direction.X < 0)
            {
                TextureDirection = TextureDirection.Left;
            }
            if (Direction.X > 0)
            {
                TextureDirection = TextureDirection.Right;
            }
        }

        public virtual void Draw(ref SpriteBatch _spriteBatch)
        {
            Sprite.Draw(ref _spriteBatch, Position, TextureDirection);
        }
    }
}
