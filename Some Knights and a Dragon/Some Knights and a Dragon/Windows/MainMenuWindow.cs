using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows
{
    class MainMenuWindow : GameWindow // The Main Menu (WORK IN PROGRESS)
    {
        List<MenuItem> menuItems; // List of the menuItems
        Sprite logo; // The logo of the game

        public MainMenuWindow() : base("Menu Window")
        {
            menuItems = new List<MenuItem>();
            menuItems.Add(new Button(new Vector2(640, 500), "Play", PlayButtonClick));
            menuItems.Add(new Button(new Vector2(640, 600), "Settings", SettingsButtonClick));
            menuItems.Add(new Button(new Vector2(640, 700), "Quit", QuitButtonClick));

            logo = new Sprite("Menus/logo");

            Game1.SongManager.Play("intro");
            Loaded = true;
        }
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            foreach (Button menuItem in menuItems)
            {
                menuItem.Draw(_spriteBatch);
            }
            logo.DrawFrame(ref _spriteBatch, new Vector2(640, 250), 0, 0);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            foreach (Button button in menuItems)
            {
                button.Update();
            }
        }

        public void PlayButtonClick()
        {
            Game1.WindowManager.GameState = GameState.Playing;
            Game1.WindowManager.LoadGameplay();
        }

        public void QuitButtonClick()
        {
            Game1.Quit = true;
        }

        public void SettingsButtonClick()
        {
            Game1.WindowManager.GameState = GameState.SettingsMainMenu;
        }
    }
}
