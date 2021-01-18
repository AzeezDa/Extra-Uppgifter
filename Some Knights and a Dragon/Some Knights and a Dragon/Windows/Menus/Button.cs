using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows.Menus
{
    public class Button
    {
        Sprite sprite;
        Vector2 position;
        bool hover;

        public delegate void OnClickDelegate();
        public OnClickDelegate OnClick { get; set; }
        

        public Button(string filepath, Vector2 position)
        {
            sprite = new Sprite("Menus/" + filepath, 2, 1);
            this.position = position;
        }
        public void Update(ref GameTime gameTime)
        {
            hover = false;
            if (sprite.GetBoundaryBoxAt(position).Contains(Game1.InputManager.GetCursor()))
                hover = true;

            if (hover && Game1.InputManager.LeftMouseClicked())
                OnClick();
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            if (hover)
                sprite.DrawFrame(ref spriteBatch, position, 0, 1);
            else
                sprite.DrawFrame(ref spriteBatch, position, 0, 0);
        }
    }
}
