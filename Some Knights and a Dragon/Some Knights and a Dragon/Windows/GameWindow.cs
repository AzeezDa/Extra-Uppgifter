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
        public static InputManager InputManager { get; set; } // Static to be accessed by everywhere without being passed into objects.
        public static FontManager FontManager { get; set; } // Static to be accessed by everywhere without being passed into the objects.
        public virtual void Update(ref GameTime gameTime) // Updates the window content
        {

        }

        public virtual void Draw(ref SpriteBatch _spriteBatch) // Draw the window content
        {

        }
    }
}
