using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows
{
    // Class used to work with windows what are mainly menu based
    public abstract class MenuWindow : GameWindow
    {

        // Dictionary of menu items. Dictionary is used to access the items through keys rather than having to loop through the list to get them
        protected Dictionary<string, MenuItem> MenuItems;

        // Constructor
        protected MenuWindow(string name) : base(name)
        {
            MenuItems = new Dictionary<string, MenuItem>();
        }

        // Draws the menu items on the screen
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            foreach (MenuItem menuItem in MenuItems.Values)
                menuItem.Draw(_spriteBatch);
        }

        // Updates every menu item
        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            foreach (MenuItem menuItem in MenuItems.Values)
                menuItem.Update();
        }
    }
}
