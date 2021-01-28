using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows.Menus
{
    public abstract class MenuItem
    {
        public Sprite Sprite { get; protected set; }
        public Vector2 Position { get; protected set; }
        public bool Hover { get; protected set; }
        public string Name { get; protected set; }

        public MenuItem(Vector2 position, string name)
        {
            Position = position;
            Name = name;
        }

        public virtual void Update()
        {
            Hover = false;
            if (Sprite.GetBoundaryBoxAt(Position).Contains(Game1.InputManager.GetCursor()))
                Hover = true;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
