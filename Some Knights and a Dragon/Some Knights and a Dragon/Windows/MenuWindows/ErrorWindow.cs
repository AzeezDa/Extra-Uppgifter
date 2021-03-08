using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class ErrorWindow : MenuWindow
    {
        // The current exception shown on the error window
        Exception currentException;

        // The error message, can be custom or from the exception handler
        string errorMessage;
        public ErrorWindow() : base("Error Window")
        {
            // Add the menu items
            MenuItems.Add("Main Menu", new Button(new Vector2(640, 800), "Main Menu", returnToMainMenu));
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            // Display the error
            Game1.FontManager.WriteTitle(_spriteBatch, "ERROR", new Vector2(640, 100));
            Game1.FontManager.WriteText(_spriteBatch, errorMessage, new Vector2(640, 300), 500);
            Game1.FontManager.WriteText(_spriteBatch, currentException.Message, new Vector2(640, 500), 500);
            
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }

        private void returnToMainMenu()
        {
            // Return the user to the main menu
            Game1.WindowManager.GameState = Managers.GameState.MainMenu;
        }

        public void NewError(Exception exception, string errorMessage)
        {
            // Called to set the displayed error to given one through the argument
            currentException = exception;
            this.errorMessage = errorMessage;
        }
    }
}
