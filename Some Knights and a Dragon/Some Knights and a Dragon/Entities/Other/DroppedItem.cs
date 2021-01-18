using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Items;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Entities
{
    public class DroppedItem : Entity
    {
        public float LifeTime { get; private set; } // How long the item is on the ground (Used for animation)

        public Item Item { get; private set; } // The item dropped

        private double animationOffset; // Offest to the sinus function used for the animation (Se Update())

        public DroppedItem(Vector2 position, Item item)
        {
            Random r = new Random();
            Item = item;
            Position = position;
            Speed = Vector2.Zero;
            LifeTime = 0;
            Sprite = item.Sprite;
            animationOffset = r.NextDouble();
        }
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            LifeTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // Changes position to give the floating animation effect using a Asin(Bx + C) + D function
            Position = new Vector2(Position.X, Game1.WindowManager.GetGameplayWindow().CurrentLevel.Boundries.Bottom + 15 * (float)Math.Sin(3 * (LifeTime + animationOffset)) - 40);
            if (CollidingWithBoundries) // Makes the item gravity less to float up and down
            {
                ObeysGravity = false;
                Velocity *= 0;
            }
        }

        protected override void LoadSprite(string filepath) // Loads the sprite
        {
            base.LoadSprite(filepath);
            Sprite = new Sprite(filepath);
        }
    }
}
