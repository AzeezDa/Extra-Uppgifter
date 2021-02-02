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
        public delegate void OnClickDelegate();
        public OnClickDelegate OnClick { get; set; }

        public Button(Vector2 position, string name, OnClickDelegate onClick) : base(position, name)
        {
            Sprite = new Sprite("Menus/button", 2, 1);
            OnClick = onClick;
        }
        public override void Update()
        {
            base.Update();
            if (Hover && Game1.InputManager.LeftMouseClicked())
                OnClick();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Hover)
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 1);
                Game1.FontManager.WriteTitle(spriteBatch, Name, Position);
            }
            else
            {
                Sprite.DrawFrame(ref spriteBatch, Position, 0, 0);
                Game1.FontManager.WriteTitle(spriteBatch, Name, Position, Color.Black);
            }

            
        }
    }
}
