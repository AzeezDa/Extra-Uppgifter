using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Creatures
{
    public enum TextureDirection { Left, Right }
    public abstract class Creature
    {
        protected Sprite Sprite;
        public int Health { get; protected set; }
        public string Name { get; protected set; }
        public Vector2 Position { get; protected set; }
        protected Vector2 Speed;
        protected Vector2 Direction;

        public Rectangle HitBox { get; protected set; }

        protected int SpriteAnimationColumn = 0;
        protected int SpriteAnimationRow = 0;

        protected TextureDirection TextureDirection;


        public Creature()
        {
        }

        public virtual void Update(ref GameTime gameTime)
        {

            HitBox = new Rectangle((int)Position.X - Sprite.Width / 2 * Sprite.Scale, (int)Position.Y - Sprite.Height / 2 * Sprite.Scale, Sprite.Width * Sprite.Scale, Sprite.Height * Sprite.Scale);
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(ref SpriteBatch _spriteBatch)
        {
            Sprite.Draw(ref _spriteBatch, Position, TextureDirection, SpriteAnimationColumn, SpriteAnimationRow);
        }

        public virtual void Attack()
        {

        }

        public virtual void TakeDamage(int amount)
        {
            Health -= amount;
        }
    }
}
