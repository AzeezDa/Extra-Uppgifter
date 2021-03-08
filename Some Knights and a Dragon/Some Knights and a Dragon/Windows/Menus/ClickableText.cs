using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows.Menus
{
    public class ClickableText : MenuItem
    {
        public delegate void OnClickDelegate();

        // The method that is called when the user is clicking on the text
        public OnClickDelegate OnClick { get; set; }

        // The text to be displayed
        public string Text { get; private set; }

        private Rectangle boundries;
        public ClickableText(Vector2 position, string name, string text, OnClickDelegate onClick) : base(position)
        {
            // Set the object's values for the correct text and method to call upon click
            Text = text;
            OnClick = onClick;

            // Get the values to correctly display the text and when it is highlighted
            Vector2 textDimensions = Game1.FontManager.DefaultFont.MeasureString(Text);
            boundries = new Rectangle((int)position.X - (int)textDimensions.X / 2, (int)position.Y - (int)textDimensions.Y / 2, (int)textDimensions.X, (int)textDimensions.Y);
        }

        public override void Update()
        {
            // Check if the player is hovering over the text
            Hover = boundries.Contains(Game1.InputManager.GetCursor());

            // If the player is hover and left clicked, then activate the method
            if (Hover && Game1.InputManager.LeftMouseClicked())
                OnClick();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draws the text and change color depending if the user is hovering
            if (Hover)
                Game1.FontManager.WriteText(spriteBatch, Text, Position, Color.Cyan);
            else
                Game1.FontManager.WriteText(spriteBatch, Text, Position);
        }

        public override void ChangePosition(Vector2 newPosition)
        {
            base.ChangePosition(newPosition);
            Vector2 textDimensions = Game1.FontManager.DefaultFont.MeasureString(Text);
            boundries = new Rectangle((int)Position.X - (int)textDimensions.X / 2, (int)Position.Y - (int)textDimensions.Y / 2, (int)textDimensions.X, (int)textDimensions.Y);
        }
    }
}
