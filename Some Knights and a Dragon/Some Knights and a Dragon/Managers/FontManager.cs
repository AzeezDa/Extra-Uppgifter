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
        public SpriteFont TitlesFont { get; private set; } // Get the bigger Font loaded by this class

        public FontManager()
        {
            // Initiate the spritefonts
            DefaultFont = Game1.ContentManager.Load<SpriteFont>("default_font");
            TitlesFont = Game1.ContentManager.Load<SpriteFont>("titles_font");
        }

        // Writes a text of small size on the screen
        public void WriteText(SpriteBatch _spriteBatch, string text, Vector2 position, Color ? color = null)
        {
            _spriteBatch.DrawString(DefaultFont,
                                    text,
                                    position - DefaultFont.MeasureString(text) / 2,
                                    color ?? Color.White);
        }

        // Writes a larger text on teh screen
        public void WriteTitle(SpriteBatch _spriteBatch, string text, Vector2 position, Color? color = null)
        {
            _spriteBatch.DrawString(TitlesFont,
                                    text,
                                    position - TitlesFont.MeasureString(text) / 2,
                                    color ?? Color.White);
        }

        // Overload of WriteText that has words wrapping if they surpass a certain length per line
        public void WriteText(SpriteBatch _spriteBatch, string text, Vector2 position, float lineLength, Color? color = null)
        {
            text = wrapText(DefaultFont, text, lineLength);

            _spriteBatch.DrawString(DefaultFont,
                                    text,
                                    position - DefaultFont.MeasureString(text) / 2,
                                    color ?? Color.White);
        }

        // The wrap text method used in the text wrapping overload of the WriteText method
        private string wrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            // Splits the words and initiate the string builder
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();

            // Current width of a single line
            float currentLineWidth = 0f;
            
            // Width of the space character with the font
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                // Measure string width in pixels
                Vector2 size = spriteFont.MeasureString(word);

                // If the pixel length of the word is not greater than the line then add the word else add to new line and reset current Line pixel width
                if (currentLineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    currentLineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    currentLineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
    }
}
