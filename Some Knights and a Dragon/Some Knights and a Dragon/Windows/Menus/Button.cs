using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows.Menus
{
    public class Button : MenuItem
    {
        // On click do this method
        public delegate void OnClickDelegate();

        // The delegate object for the click
        public OnClickDelegate OnClick { get; set; }

        // The text displayed on the button
        public string Text { get; private set; }

        public Button(Vector2 position, string text, OnClickDelegate onClick) : base(position)
        {
            // Load the sprite and set the initial values based on the passed arguments
            Sprite = new Sprite("Menus/button", 2, 1);
            OnClick = onClick;
            Text = text;
        }
        public override void Update()
        {
            base.Update();
            
            // If the user is hover and left clicking then the OnClick method is called
            if (Hover && Game1.InputManager.LeftMouseClicked())
                OnClick();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // If hovering, change the look of the button
            if (Hover)
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 1);
                Game1.FontManager.WriteTitle(spriteBatch, Text, Position);
            }
            else // Else display the normal look
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 0);
                Game1.FontManager.WriteTitle(spriteBatch, Text, Position, Color.Black);
            }

            
        }
    }
}
