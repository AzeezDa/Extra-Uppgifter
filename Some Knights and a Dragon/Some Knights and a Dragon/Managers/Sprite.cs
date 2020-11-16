using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Entities;
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

        Timer animationTimer;
        private double currentAnimationDuration = 0;

        int currentRow = 0;
        int currentColumn = 0;

        int fromColumn = 0;
        int fromRow = 0;
        int toColumn = 0;
        int toRow = 0;

        public Sprite(string filepath)
        {
            spriteTexture = Game1.ContentManager.Load<Texture2D>(filepath);

            Width = spriteTexture.Width;
            Height = spriteTexture.Height;

            animationTimer = new Timer(1000 / 60);
        }

        public Sprite(string filepath, int width, int height)
        {
            spriteTexture = Game1.ContentManager.Load<Texture2D>(filepath);
            Width = width;
            Height = height;
            animationTimer = new Timer(1000 / 60);
        }

        public void Animate(int fromColumn, int toColumn, int fromRow, int toRow, double duration)
        {
            this.fromColumn = fromColumn;
            this.toColumn = toColumn;
            this.fromRow = fromRow;
            this.toRow = toRow;
            currentAnimationDuration = duration;
        }

        public void Animate(int column, int row, double duration)
        {
            fromColumn = column;
            toColumn = column;
            fromRow = row;
            toRow = row;
            currentAnimationDuration = duration;
        }

        public void Update(ref GameTime gameTime)
        {
            animationTimer.CheckTimer(ref gameTime);
            if (currentAnimationDuration > 0)
            {
                currentAnimationDuration -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (animationTimer.TimerOn)
                {
                    currentColumn = (currentColumn + 1) % (toColumn - fromColumn + 1);
                    currentRow = (currentRow + 1) % (toRow - fromRow + 1);
                }
            }
            else
            {
                fromColumn = 0;
                fromRow = 0;
                currentColumn = 0;
                currentRow = 0;
            }
        }

        public void Draw(ref SpriteBatch spriteBatch, Vector2 position, TextureDirection textureDirection, float rotation = 0f)
        {
            spriteBatch.Draw(
                spriteTexture,
                new Rectangle((int)position.X, (int)position.Y, Width * Scale, Height * Scale),
                new Rectangle((fromRow + currentRow) * Width, (fromColumn + currentColumn) * Height, Width, Height),
                Color.White,
                rotation,
                new Vector2(Width / 2, Height / 2),
                textureDirection == 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0);
        }

    }
}
