using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers
{
    public enum GameState { MainMenu, Playing, Paused };
    public class WindowManager
    {
        private MainMenuWindow mainMenuWindow;
        private GameplayWindow gameplayWindow;
        private PauseWindow pauseWindow;

        
        public GameState GameState { get; set; }
        public WindowManager()
        {
            mainMenuWindow = new MainMenuWindow();

            GameState = new GameState();
        }

        public void Update(GameTime gameTime)
        {
            switch (GameState)
            {
                case GameState.MainMenu:
                    mainMenuWindow.Update(ref gameTime);
                    break;
                case GameState.Playing:
                    gameplayWindow.Update(ref gameTime);
                    break;
                case GameState.Paused:
                    pauseWindow.Update(ref gameTime);
                    break;
                default:
                    break;
            }

            if (Game1.InputManager.KeyClicked(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                switch (GameState)
                {
                    case GameState.Playing:
                        GameState = GameState.Paused;
                        Game1.SongManager.Pause();
                        break;
                    case GameState.Paused:
                        GameState = GameState.Playing;
                        Game1.SongManager.Resume();
                        break;
                    default:
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (GameState)
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
                default:
                    break;
            }
        }

        public GameplayWindow GetGameplayWindow() // Gets the gameplay window. UNSTABLE IF IS USED WHILE CURRENT WINDOW IS NOT A GAMEPLAYWINDOW
        {
            return gameplayWindow;
        }


        public void LoadGameplay()
        {
            gameplayWindow = new GameplayWindow();
            pauseWindow = new PauseWindow();
            gameplayWindow.LoadContent();
            pauseWindow.LoadContent();
        }

        public void UnloadGameplay()
        {
            gameplayWindow = null;
            pauseWindow = null;
            GC.Collect();
        }
    }
}
