using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Managers;
using Microsoft.Xna.Framework.Input;

namespace Some_Knights_and_a_Dragon.Items
{
    public abstract class Item
    {
        public Sprite Sprite { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Vector2 Handle { get; protected set; }

        public Item()
        {
        }

        protected virtual void LoadSprite(string filePath, int width, int height)
        {
            Sprite = new Sprite(filePath, width, height, 12);
        }

        protected virtual void LoadSprite(string filePath)
        {
            Sprite = new Sprite(filePath);
        }

        public virtual void Update(GameTime gameTime)
        {
            Sprite.Update(ref gameTime);
        }
        public virtual void OnUse()
        {

        }

        public virtual void ResetSprite()
        {
            Sprite.Unfreeze();
        }

        public void DrawOn(ref SpriteBatch spriteBatch, Vector2 position, Entities.TextureDirection textureDirection, float rotation = 0)
        {

            Sprite.Draw(ref spriteBatch,
                position + Handle * (textureDirection == Entities.TextureDirection.Right ? new Vector2(-1,1) : Vector2.One),
                textureDirection, 
                rotation);
        }
    }
}
