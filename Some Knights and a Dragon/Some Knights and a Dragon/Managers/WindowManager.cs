using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class WindowManager
    {
        public GameWindow CurrentWindow { get; private set; } // Currently displayed window

        public WindowManager()
        {
            CurrentWindow = new MenuWindow();
        }

        public void Update(GameTime gameTime)
        {
            CurrentWindow.Update(ref gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWindow.Draw(ref spriteBatch);
        }

        public void NewWindow(GameWindow gameWindow) // Changes the current window
        {
            CurrentWindow = gameWindow;
            CurrentWindow.LoadContent();
        }

        public GameplayWindow GetGameplayWindow() // Gets the gameplay window. UNSTABLE IF IS USED WHILE CURRENT WINDOW IS NOT A GAMEPLAYWINDOW
        {
            return (GameplayWindow)CurrentWindow;
        }
    }
}
