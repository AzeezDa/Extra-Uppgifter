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
        // The sprite of the menu item
        public Sprite Sprite { get; protected set; }

        // The position on the screen
        public Vector2 Position { get; protected set; }

        // Bool to store if the item is being hovered over
        public bool Hover { get; protected set; }

        public MenuItem(Vector2 position)
        {
            // Set the value based on argument
            Position = position;
        }

        public virtual void Update()
        {
            // Update the hover value by checking if the mouse vector is within the rectangle of the menu item
            Hover = false;
            if (Sprite.GetBoundaryBoxAt(Position).Contains(Game1.InputManager.GetCursor()))
                Hover = true;
        }

        // Abstract method to display the menu item, must be implemented
        public abstract void Draw(SpriteBatch spriteBatch);

        // Change the position of the menu item
        public virtual void ChangePosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
    }
}
