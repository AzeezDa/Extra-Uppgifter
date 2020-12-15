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
        public float LifeTime { get; private set; }

        public Item Item { get; private set; }

        public DroppedItem(Vector2 position, Item item)
        {
            Item = item;
            Position = position;
            Speed = Vector2.Zero;
            LifeTime = 0;
            Sprite = item.Sprite;
        }
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            LifeTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            Position = new Vector2(Position.X, ((GameplayWindow)Game1.CurrentWindow).CurrentGameArea.Boundries.Bottom + 15 * (float)Math.Sin(3 * LifeTime) - 40);
            if (CollidingWithBoundries)
            {
                ObeysGravity = false;
                Velocity *= 0;
            }
        }

        protected override void LoadSprite(string filepath)
        {
            base.LoadSprite(filepath);
            Sprite = new Sprite(filepath + "lmao");
        }
    }
}
