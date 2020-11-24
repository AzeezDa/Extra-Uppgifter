using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class FontManager
    {

        // THIS CLASS IS TO BE CHANGED TO ADD DIFFERENT FONTS FOR DIFFERENT SITUATIONS
        public SpriteFont DefaultFont { get; private set; } // Get the Default Font loaded by this class

        public FontManager(string fontName = "default_font")
        {
            DefaultFont = Game1.ContentManager.Load<SpriteFont>(fontName);
        }
    }
}
