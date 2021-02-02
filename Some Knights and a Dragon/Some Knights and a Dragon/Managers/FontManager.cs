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
        private SpriteFont defaultFont; // Get the Default Font loaded by this class
        private SpriteFont titlesFont; // Get the bigger Font loaded by this class

        public FontManager(string fontName = "default_font")
        {
            defaultFont = Game1.ContentManager.Load<SpriteFont>(fontName);
            titlesFont = Game1.ContentManager.Load<SpriteFont>("titles_font");
        }

        public void WriteText(SpriteBatch _spriteBatch, string text, Vector2 position, Color ? color = null)
        {
            _spriteBatch.DrawString(defaultFont,
                                    text,
                                    position - defaultFont.MeasureString(text) / 2,
                                    color ?? Color.White);
        }

        public void WriteTitle(SpriteBatch _spriteBatch, string text, Vector2 position, Color? color = null)
        {
            _spriteBatch.DrawString(titlesFont,
                                    text,
                                    position - titlesFont.MeasureString(text) / 2,
                                    color ?? Color.White);
        }
    }
}
