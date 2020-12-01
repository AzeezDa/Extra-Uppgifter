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
        public Texture2D SpriteTexture { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Scale = 5;

        Timer animationTimer;

        int currentFrame = 0;
        int currentRow = 0;
        int frames = 1;
        bool oneTimeAnimationOn = false;
        bool freeze = false;

        // Load a sprite with no other animations
        public Sprite(string filepath)
        {
            SpriteTexture = Game1.ContentManager.Load<Texture2D>(filepath);

            Width = SpriteTexture.Width;
            Height = SpriteTexture.Height;

            animationTimer = new Timer(1000 / 60);
        }

        // Load spritesheet and work on it
        public Sprite(string filepath, int width, int height, int framesPerSecond = 12)
        {
            SpriteTexture = Game1.ContentManager.Load<Texture2D>(filepath);
            Width = width;
            Height = height;
            animationTimer = new Timer(1000 / framesPerSecond);
        }

        // New texture
        public void NewTexture(Texture2D texture, int width, int height)
        {
            SpriteTexture = texture;
            Width = width;
            Height = height;
        }

        // Animate certain row and how many frames in the row, forever
        public void Animate(int row, int frames)
        {
            if (!oneTimeAnimationOn)
            {
                currentRow = row;
                this.frames = frames;
            }
        }

        // Animate certain row and how many frames in the row, once
        public void OneTimeAnimation(int row, int frames)
        {
            if (!oneTimeAnimationOn)
            {
                oneTimeAnimationOn = true;
                currentFrame = 0;
                currentRow = row;
                this.frames = frames;
            }
        }

        // Freeze the sprite at a certain frame.
        public void Freeze(int row, int frames)
        {
            if (!oneTimeAnimationOn)
            {
                oneTimeAnimationOn = true;
                currentFrame = 0;
                currentRow = row;
                this.frames = frames;
                freeze = true;
            }
        }

        // Unfreeze the sprite from the Freeze method
        public void Unfreeze()
        {
            oneTimeAnimationOn = false;
            freeze = false;
            currentRow = 0;
            currentFrame = 0;
            frames = 1;
        }
        
        // Update the sprite
        public void Update(ref GameTime gameTime)
        {
            animationTimer.CheckTimer(ref gameTime);
            if (animationTimer.TimerOn && oneTimeAnimationOn && freeze)
            {
                currentFrame = (currentFrame == frames - 1) ? currentFrame : (currentFrame + 1);
            }
            else if (animationTimer.TimerOn && !oneTimeAnimationOn && !freeze)
            {
                currentFrame = (currentFrame + 1) % frames;
            }
            else if (animationTimer.TimerOn && oneTimeAnimationOn && !freeze)
            {
                if (currentFrame >= frames - 1)
                {
                    oneTimeAnimationOn = false;
                    currentRow = 0;
                    currentFrame = 0;
                    frames = 1;
                }
                else
                    currentFrame++;
            }
        }

        // Draws the sprite
        public void Draw(ref SpriteBatch spriteBatch, Vector2 position, TextureDirection textureDirection, float rotation = 0f)
        {
            spriteBatch.Draw(
                SpriteTexture,
                new Rectangle((int)position.X, (int)position.Y, Width * Scale, Height * Scale),
                new Rectangle(currentFrame * Width, currentRow * Height, Width, Height),
                Color.White,
                rotation,
                new Vector2(Width / 2, Height / 2),
                textureDirection == 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0);
        }

    }
}
