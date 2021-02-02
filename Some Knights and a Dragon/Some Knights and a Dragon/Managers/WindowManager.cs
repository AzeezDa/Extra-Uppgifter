using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public enum GameState { MainMenu, Playing, Paused, Dead, SettingsMainMenu, SettingsInGame };
    public class WindowManager
    {
        private MainMenuWindow mainMenuWindow; // The starting menu window
        private GameplayWindow gameplayWindow; // The window where the game is played
        private PauseWindow pauseWindow; // When the game is paused this window is displayed
        private DeathWindow deathWindow; // When the player dies this window is displayed
        private SettingsWindow settingsWindow; // Where the settings are

        public GameState GameState { get; set; } // The different states of the game
        public WindowManager()
        {
            mainMenuWindow = new MainMenuWindow();
            settingsWindow = new SettingsWindow();
            GameState = new GameState();
        }

        public void Update(GameTime gameTime)
        {
            switch (GameState) // Updates based on gamestates
            {
                case GameState.MainMenu:
                    mainMenuWindow.Update(ref gameTime); // Main Menu
                    break;
                case GameState.Playing:
                    gameplayWindow.Update(ref gameTime); // Gameplay
                    break;
                case GameState.Paused:
                    pauseWindow.Update(ref gameTime); // Pause menu
                    break;
                case GameState.Dead:
                    deathWindow.Update(ref gameTime); // Death menu
                    break;
                case GameState.SettingsInGame:
                case GameState.SettingsMainMenu:
                    settingsWindow.Update(ref gameTime); // Settings if in game or in main menu
                    break;
                default:
                    break;
            }

            if (Game1.InputManager.KeyClicked(Microsoft.Xna.Framework.Input.Keys.Escape)) // If escape is clicked then
            {
                switch (GameState)
                {
                    case GameState.Playing:
                        GameState = GameState.Paused;  // Playing -> Paused
                        Game1.SongManager.Pause();
                        break;
                    case GameState.Paused:
                        GameState = GameState.Playing; // Paused -> Playing
                        Game1.SongManager.Resume();
                        break;
                    case GameState.SettingsInGame:
                        GameState = GameState.Paused; // If in game and in settings -> Paused
                        settingsWindow.SaveSettings();
                        break;
                    case GameState.SettingsMainMenu:
                        GameState = GameState.MainMenu; // If in menu and in settings -> main menu
                        settingsWindow.SaveSettings();
                        break;
                    default:
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (GameState) // Draws the different windows based on states
            {
                case GameState.MainMenu:
                    mainMenuWindow.Draw(ref spriteBatch);
                    break;
                case GameState.Playing:
                    gameplayWindow.Draw(ref spriteBatch);
                    break;
                case GameState.Paused:
                    gameplayWindow.Draw(ref spriteBatch);
                    pauseWindow.Draw(ref spriteBatch);
                    break;
                case GameState.Dead:
                    gameplayWindow.Draw(ref spriteBatch);
                    deathWindow.Draw(ref spriteBatch);
                    break;
                case GameState.SettingsInGame:
                    gameplayWindow.Draw(ref spriteBatch);
                    settingsWindow.Draw(ref spriteBatch);
                    break;
                case GameState.SettingsMainMenu:
                    settingsWindow.Draw(ref spriteBatch);
                    break;
                default:
                    break;
            }
        }

        public GameplayWindow GetGameplayWindow() // Gets the gameplay window. UNSTABLE IF IS USED WHILE CURRENT WINDOW IS NOT A GAMEPLAYWINDOW
        {
            return gameplayWindow;
        }


        public void LoadGameplay() // Loads the gameplay window and its content
        {
            gameplayWindow = new GameplayWindow();
            pauseWindow = new PauseWindow();
            deathWindow = new DeathWindow();
            gameplayWindow.LoadContent();
            pauseWindow.LoadContent();
        }

        public void UnloadGameplay() // Removes all gameplay windows and their content
        {
            gameplayWindow = null;
            pauseWindow = null;
            deathWindow = null;
            GC.Collect();
        }
    }
}
