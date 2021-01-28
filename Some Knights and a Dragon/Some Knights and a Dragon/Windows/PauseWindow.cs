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
    public class PauseWindow : GameWindow
    {
        List<MenuItem> menuItems;
        public PauseWindow() : base("Pause Window")
        {
            menuItems = new List<MenuItem>();
            menuItems.Add(new Button("resumeButton", new Vector2(640, 300), "Resume Button", ResumeButton));
            menuItems.Add(new Button("mainMenuButton", new Vector2(640, 400), "Main Menu Button", BackToMainMenu));
            menuItems.Add(new Slider(new Vector2(640, 600), "Volume Slider"));
        }
        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            _spriteBatch.Draw(Game1.TextureManager.BlankTexture, new Rectangle(0, 0, 1280, 960), Color.Gray * 0.5f); // Draws a faint foreground on the screen

            Game1.FontManager.WriteText(_spriteBatch, "PAUSED", new Vector2(640, 300));
            Game1.FontManager.WriteText(_spriteBatch, "MUSIC VOLUME", new Vector2(640, 550));

            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.Draw(_spriteBatch);
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Loaded = true;
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.Update();
                if (menuItem.Name == "Volume Slider")
                    MediaPlayer.Volume = ((Slider)menuItem).SliderValue;
            }
        }

        public void ResumeButton()
        {
            Game1.WindowManager.GameState = Managers.GameState.Playing;
        }

        public void BackToMainMenu()
        {
            Game1.WindowManager.GameState = Managers.GameState.MainMenu;
            Game1.WindowManager.UnloadGameplay();
            Game1.SongManager.Play("intro");
        }
    }
}
