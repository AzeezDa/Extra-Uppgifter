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
        protected Sprite Sprite;
        public string Name { get; protected set; }
        public Vector2 Position { get; protected set; }
        protected Vector2 Speed;
        protected Vector2 Direction;

        public Rectangle HitBox { get; protected set; }

        protected int HitBoxWidth = 0;
        protected int HitBoxHeight = 0;

        protected int SpriteAnimationColumn = 0;
        protected int SpriteAnimationRow = 0;

        protected TextureDirection TextureDirection;

        public virtual void Update(ref GameTime gameTime)
        {

            HitBox = new Rectangle(
                (int)Position.X - HitBoxWidth * Sprite.Scale / 2,
                (int)Position.Y - HitBoxHeight * Sprite.Scale / 2,
                HitBoxWidth * Sprite.Scale,
                HitBoxHeight * Sprite.Scale
                );
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            Texture2D t = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
            t.SetData(new Color[] { Color.Red });
            _spriteBatch.Draw(t, HitBox, Color.Red);
            Sprite.Draw(ref _spriteBatch, Position, TextureDirection, SpriteAnimationColumn, SpriteAnimationRow);
        }
    }
}
