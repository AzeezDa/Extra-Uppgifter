using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows.Menus
{
    class TextBox : MenuItem
    {
        // Text in the textbox
        public string Text { get; private set; }

        // Text that is shown when the box is empty and inactive
        private string backgroundText;

        // If the box has been clicked
        bool active;

        public TextBox(Vector2 position, string name, string defaultText = "", string backgroundText = "") : base(position)
        {
            Text = defaultText; // If there is some default text to be in the box
            this.backgroundText = backgroundText;
            Sprite = new Managers.Sprite("Menus/sliderBackground", 2, 1);
            Game1.GameWindow.TextInput += TextInputHandler; // Sets the input handler as a delegate the text input event handler
        }

        private void TextInputHandler(object sender, TextInputEventArgs e)
        {
            // If the character pressed is a character is printable by the font manager then add to text
            if (active && Game1.FontManager.DefaultFont.Characters.Contains(e.Character))
                Text += e.Character;
            else if (active && e.Key == Keys.Back && Text.Length > 0) // else if the key is backspace then remove the last character
                Text = Text.Remove(Text.Length - 1);
        }

        public override void Update()
        {
            base.Update();
            
            // If the the mouse is clicked then the active boolean is set to the hover bool value which means:
            // If the the mouse is clicked while hovering on the box then the box is active
            if (Game1.InputManager.LeftMouseClicked())
                active = Hover;
        }

        
        public override void Draw(SpriteBatch spriteBatch)
        {
            // If active then draw the highlighted version of the box, else the non highlighted one is displayed
            if (active) 
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 1);
            else
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 0);

            // If there is no text in the box a default background text is displayed, else display the actual text
            if (Text.Length == 0) 
                Game1.FontManager.WriteText(spriteBatch, backgroundText, Position, Color.DarkGray);
            else
                Game1.FontManager.WriteText(spriteBatch, Text, Position);
        }
    }
}
