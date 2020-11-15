using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class Sprite
    {
        Texture2D spriteTexture;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Scale = 5;

        public Sprite(string filepath)
        {
            spriteTexture = Game1.ContentManager.Load<Texture2D>(filepath);

            Width = spriteTexture.Width;
            Height = spriteTexture.Height;
        }

        public Sprite(string filepath, int width, int height)
        {
            spriteTexture = Game1.ContentManager.Load<Texture2D>(filepath);
            Width = width;
            Height = height;
        }

        public void Draw(ref SpriteBatch spriteBatch, Vector2 position, TextureDirection textureDirection, int column = 0, int row = 0)
        {
            spriteBatch.Draw(
                spriteTexture,
                new Rectangle((int)position.X, (int)position.Y, Width * Scale, Height * Scale),
                new Rectangle(row * Width, column * Height, Width, Height),
                Color.White,
                0f,
                new Vector2(Width / 2, Height / 2),
                textureDirection == 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0);
        }

    }
}
