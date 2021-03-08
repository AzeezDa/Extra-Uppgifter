using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows.Menus
{
    public class Slider : MenuItem
    {
        // The slider button sprite
        Sprite sliderButton; 

        // The current value of the slider, between 0 and 1
        public float SliderValue { get; private set; }

        // Informative text about the slider, displayed above the slider
        public string Text { get; private set; }
        public Slider(Vector2 position, string text, float defaultValue = 1) : base(position)
        {
            // Load the slider background sprite and set its scale to 3
            Sprite = new Sprite("Menus/sliderBackground", 2, 1);
            Sprite.Scale = 3;

            // Load the slider button sprite and set its scale to 3
            sliderButton = new Sprite("Menus/sliderButton", 2, 1);
            sliderButton.Scale = 3;

            // Set the values according to the arguments
            SliderValue = defaultValue;
            Text = text;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Game1.FontManager.WriteTitle(spriteBatch, Text, Position + new Vector2(0, -50)); // Writes the name of the slider above the slider
            if (Hover) // If hovering change the look of the slider
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 1);
                sliderButton.DrawFrame(ref spriteBatch, new Vector2(Position.X - Sprite.ScaledWidth / 2 + SliderValue * Sprite.ScaledWidth, Position.Y), 0, 1);
            }
            else // Else display the normal look
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 0);
                sliderButton.DrawFrame(ref spriteBatch, new Vector2(Position.X - Sprite.ScaledWidth / 2 + SliderValue * Sprite.ScaledWidth, Position.Y), 0, 0);
            }
                
        }

        public override void Update()
        {
            base.Update();
            if (Hover) // If hovering then updates the slider value
            {
                if (Game1.InputManager.LeftMousePressed())
                {
                    SliderValue = (Game1.InputManager.GetCursor().X - Position.X + Sprite.ScaledWidth / 2) / Sprite.ScaledWidth;
                }
            }
        }
    }
}
