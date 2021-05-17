using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public enum GameState { MainMenu, Playing, Paused, Dead, SettingsMainMenu, SettingsInGame, Error,
                            HighScore, PrePlay, NewGame, ContinueGame, LocalPreplay, LocalLobby, LocalEnd };
    public class WindowManager
    {
        Texture2D background;

        public Dictionary<string, GameWindow> Windows;
        Sprite ConnectionStatus;
        public GameState GameState { get; set; } // The different states of the game
        public WindowManager()
        {
            Windows = new Dictionary<string, GameWindow>();
            Windows.Add("Main Menu", new MainMenuWindow());
            Windows.Add("Settings", new SettingsWindow());
            Windows.Add("Error", new ErrorWindow());
            Windows.Add("High Score", new HighScoreWindow());
            Windows.Add("Pre Play", new PrePlayWindow());
            Windows.Add("New Game", new NewGameWindow());
            Windows.Add("Local Preplay", new LocalPrePlayWindow());
            Windows.Add("Local Lobby", new LocalLobbyWindow());
            Windows.Add("Local End", new LocalGameEndWindow());
            GameState = new GameState();
            background = Game1.TextureManager.GetTexture("Backgrounds/mainBackground");
            ConnectionStatus = new Sprite("Menus/connectionStatus", 2, 1);
            ConnectionStatus.Scale = 2;
        }

        public void Update(GameTime gameTime)
        {
            switch (GameState) // Updates based on gamestates
            {
                case GameState.MainMenu:
                    Windows["Main Menu"].Update(ref gameTime); // Main Menu
                    break;
                case GameState.Playing:
                    Windows["Gameplay"].Update(ref gameTime); // Gameplay
                    break;
                case GameState.Paused:
                    Windows["Pause"].Update(ref gameTime); // Pause menu
                    break;
                case GameState.Dead:
                    Windows["Death"].Update(ref gameTime); // Death menu
                    break;
                case GameState.SettingsInGame:
                case GameState.SettingsMainMenu:
                    Windows["Settings"].Update(ref gameTime); // Settings if in game or in main menu
                    break;
                case GameState.Error:
                    Windows["Error"].Update(ref gameTime); // Error window, displayed when an error occurs
                    break;
                case GameState.HighScore:
                    Windows["High Score"].Update(ref gameTime); // Display the highscore menu
                    break;
                case GameState.PrePlay:
                    Windows["Pre Play"].Update(ref gameTime); // Display the window to choose how to play
                    break;
                case GameState.NewGame:
                    Windows["New Game"].Update(ref gameTime); // Display the window to create new game
                    break;
                case GameState.LocalPreplay:
                    Windows["Local Preplay"].Update(ref gameTime);
                    break;
                case GameState.LocalLobby:
                    Windows["Local Lobby"].Update(ref gameTime);
                    break;
                case GameState.LocalEnd:
                    Windows["Local End"].Update(ref gameTime);
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
                        ((SettingsWindow)Windows["Settings"]).SaveSettings();
                        break;
                    case GameState.SettingsMainMenu:
                        GameState = GameState.MainMenu; // If in menu and in settings -> main menu
                        ((SettingsWindow)Windows["Settings"]).SaveSettings();
                        break;
                    case GameState.HighScore:
                        GameState = GameState.MainMenu; // Change from the highscore menu to the main menu
                        break;
                    case GameState.PrePlay:
                        GameState = GameState.MainMenu; // Return to main menu
                        break;
                    case GameState.NewGame:
                        GameState = GameState.PrePlay; // Return to play play
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
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["Main Menu"].Draw(ref spriteBatch);
                    break;
                case GameState.Playing:
                    Windows["Gameplay"].Draw(ref spriteBatch);
                    break;
                case GameState.Paused:
                    Windows["Gameplay"].Draw(ref spriteBatch);
                    Windows["Pause"].Draw(ref spriteBatch);
                    break;
                case GameState.Dead:
                    Windows["Gameplay"].Draw(ref spriteBatch);
                    Windows["Death"].Draw(ref spriteBatch);
                    break;
                case GameState.SettingsInGame:
                    Windows["Gameplay"].Draw(ref spriteBatch);
                    Windows["Settings"].Draw(ref spriteBatch);
                    break;
                case GameState.SettingsMainMenu:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["Settings"].Draw(ref spriteBatch);
                    break;
                case GameState.Error:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.Red);
                    Windows["Error"].Draw(ref spriteBatch);
                    break;
                case GameState.HighScore:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["High Score"].Draw(ref spriteBatch);
                    break;
                case GameState.PrePlay:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["Pre Play"].Draw(ref spriteBatch); 
                    break;
                case GameState.NewGame:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["New Game"].Draw(ref spriteBatch);
                    break;
                case GameState.LocalPreplay:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["Local Preplay"].Draw(ref spriteBatch);
                    break;
                case GameState.LocalLobby:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["Local Lobby"].Draw(ref spriteBatch);
                    break;
                case GameState.LocalEnd:
                    spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 960), Color.White);
                    Windows["Local End"].Draw(ref spriteBatch);
                    break;
                default:
                    break;
            }

            if (NetworkClient.Connected)
                ConnectionStatus.DrawFrame(ref spriteBatch, new Vector2(50, 50), 0, 0);
            else
                ConnectionStatus.DrawFrame(ref spriteBatch, new Vector2(50, 50), 0, 1);
        }

        public GameplayWindow GetGameplayWindow() // Gets the gameplay window. UNSTABLE IF IS USED WHILE CURRENT WINDOW IS NOT A GAMEPLAYWINDOW
        {
            return (GameplayWindow)Windows["Gameplay"];
        }


        public void LoadGameplay() // Loads the gameplay window and its content
        {
            if (Windows.ContainsKey("Gameplay"))
            {
                Windows["Gameplay"] = new GameplayWindow();
                Windows["Pause"] = new PauseWindow();
                Windows["Death"] = new DeathWindow();
            }
            else
            {
                 Windows.Add("Gameplay", new GameplayWindow());
                 Windows.Add("Pause", new PauseWindow());
                 Windows.Add("Death", new DeathWindow());
            }
            
            Windows["Pause"].LoadContent();
        }

        public void UnloadGameplay() // Removes all gameplay windows and their content
        {
            Windows["Gameplay"] = null;
            Windows["Pause"] = null;
            Windows["Death"] = null;
            GC.Collect();
        }

        public void DisplayError(Exception exception, string errorMessage) // Show the error on screen on a new window
        {
            GameState = GameState.Error;
            ((ErrorWindow)Windows["Error"]).NewError(exception, errorMessage);
        }

        public void DisplayError(Exception exception) // Overload of DisplayError with a default error message
        {
            DisplayError(exception, "An Error has occured.");
        }
    }
}
