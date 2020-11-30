using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Item
{
    public abstract class Item
    {
        public Texture2D Texture { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Item(string name, string filePath)
        {
            Name = name;
            Texture = Game1.ContentManager.Load<Texture2D>(filePath);
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
