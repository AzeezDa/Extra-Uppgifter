using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Managers;

namespace Some_Knights_and_a_Dragon.Items
{
    public abstract class Item
    {
        public Sprite Sprite { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

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

        }
    }
}
