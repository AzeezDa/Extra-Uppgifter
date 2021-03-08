using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class PauseWindow : MenuWindow
    {
        public PauseWindow() : base("Pause Window")
        {
            // Add the buttons
            MenuItems.Add("Resume", new Button(new Vector2(640, 400), "Resume", ResumeButton));
            MenuItems.Add("Settings", new Button(new Vector2(640, 500), "Settings", OpenSettings));
            MenuItems.Add("Save Exit", new Button(new Vector2(640, 700), "Save and Exit", BackToMainMenu));
        }
        public override void Draw(ref SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(Game1.TextureManager.BlankTexture, new Rectangle(0, 0, 1280, 960), Color.Gray * 0.5f); // Draws a faint foreground on the screen
            base.Draw(ref _spriteBatch);
            
            // Writes the title of the window on the screen
            Game1.FontManager.WriteTitle(_spriteBatch, "PAUSED", new Vector2(640, 300));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Loaded = true;
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }

        public void ResumeButton()
        {
            // Set the game state to playing and resume the music
            Game1.WindowManager.GameState = Managers.GameState.Playing;
            Game1.SongManager.Resume();
            
        }

        public void BackToMainMenu()
        {
            // Save the game
            Game1.WindowManager.GetGameplayWindow().SaveGame();

            // Change gamestate to mainmenu
            Game1.WindowManager.GameState = Managers.GameState.MainMenu;

            // Unload the gameplay data
            Game1.WindowManager.UnloadGameplay();

            // Play the main menu music
            Game1.SongManager.Play("intro");
        }

        public void OpenSettings()
        {
            // Change the game state to settings
            Game1.WindowManager.GameState = Managers.GameState.SettingsInGame;
        }
    }
}
