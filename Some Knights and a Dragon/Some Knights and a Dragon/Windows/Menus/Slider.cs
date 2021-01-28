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
        Sprite sliderButton;
        public float SliderValue { get; private set; }
        public Slider(Vector2 position, string name, float defaultValue = 1) : base(position, name)
        {
            Sprite = new Sprite("Menus/sliderBackground", 2, 1);
            Sprite.Scale = 3;
            sliderButton = new Sprite("Menus/sliderButton", 2, 1);
            sliderButton.Scale = 3;
            SliderValue = defaultValue;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Hover)
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 1);
                sliderButton.DrawFrame(ref spriteBatch, new Vector2(Position.X - Sprite.ScaledWidth / 2 + SliderValue * Sprite.ScaledWidth, Position.Y), 0, 1);
            }
            else
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 0);
                sliderButton.DrawFrame(ref spriteBatch, new Vector2(Position.X - Sprite.ScaledWidth / 2 + SliderValue * Sprite.ScaledWidth, Position.Y), 0, 0);
            }
                
        }

        public override void Update()
        {
            base.Update();
            if (Hover)
            {
                if (Game1.InputManager.LeftMousePressed())
                {
                    SliderValue = (Game1.InputManager.GetCursor().X - Position.X + Sprite.ScaledWidth / 2) / Sprite.ScaledWidth;
                }
            }
        }
    }
}
