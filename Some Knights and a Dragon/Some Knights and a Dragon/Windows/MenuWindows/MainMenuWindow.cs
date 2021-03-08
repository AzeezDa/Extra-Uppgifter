using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows
{
    class MainMenuWindow : MenuWindow // The Main Menu (WORK IN PROGRESS)
    {
        Sprite logo; // The logo of the game

        public MainMenuWindow() : base("Menu Window")
        {

            // Add the buttons
            MenuItems.Add("Play", new Button(new Vector2(640, 500), "Play", PlayButtonClick));
            MenuItems.Add("Settings", new Button(new Vector2(640, 600), "Settings", SettingsButtonClick));
            MenuItems.Add("High Score", new Button(new Vector2(640, 700), "High Score", HighScoreButtonClick));
            MenuItems.Add("Quit", new Button(new Vector2(640, 800), "Quit", QuitButtonClick));

            // Load the logo of the game
            logo = new Sprite("Menus/logo");

            // Play the main menu music
            Game1.SongManager.Play("intro");
            Loaded = true;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            // Draw the logo
            logo.DrawFrame(ref _spriteBatch, new Vector2(640, 250), 0, 0);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }

        public void PlayButtonClick()
        {
            // Player plays
            Game1.WindowManager.GameState = GameState.PrePlay;
            ((PrePlayWindow)Game1.WindowManager.Windows["Pre Play"]).GetSaves();
        }

        public void QuitButtonClick()
        {
            // Quit the game
            Game1.Quit = true;
        }

        public void SettingsButtonClick()
        {
            // Opens settings
            Game1.WindowManager.GameState = GameState.SettingsMainMenu;
        }

        private void HighScoreButtonClick()
        {
            // Opens high score window
            Game1.WindowManager.GameState = GameState.HighScore;
            Game1.WindowManager.Windows["High Score"].LoadContent();
        }
    }
}
