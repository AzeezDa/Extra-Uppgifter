using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon
{
    public class GameWindow
    {
        public bool Loaded { get; protected set; }
        public string Name { get; private set; }

        public GameWindow(string name)
        {
            Loaded = false;
            Name = name;
        }
        public virtual void LoadContent() // Loads the content of the window
        {

        }
        public virtual void Update(ref GameTime gameTime) // Updates the window content
        {

        }

        public virtual void Draw(ref SpriteBatch _spriteBatch) // Draw the window content
        {

        }
    }
}
