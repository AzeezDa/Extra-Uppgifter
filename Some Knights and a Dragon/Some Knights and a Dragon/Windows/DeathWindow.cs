using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class DeathWindow : GameWindow
    {
        List<MenuItem> menuItems;
        public DeathWindow() : base("Death Window")
        {
            menuItems = new List<MenuItem>();
            menuItems.Add(new Button(new Vector2(640, 500), "Respawn", RespawnPlayer));
            menuItems.Add(new Button(new Vector2(640, 700), "Main Menu", BackToMain));
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            _spriteBatch.Draw(Game1.TextureManager.BlankTexture, new Rectangle(0, 0, 1280, 960), Color.Crimson * 0.7f); // Draws a faint foreground on the screen

            Game1.FontManager.WriteTitle(_spriteBatch, "YOU ARE DEAD", new Vector2(640, 400), Color.Black);

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
            }
        }

        public void RespawnPlayer() // Resets the level and changes the gamestate to playing
        {
            Game1.WindowManager.GetGameplayWindow().ResetLevel();
            Game1.WindowManager.GameState = Managers.GameState.Playing;
        }

        public void BackToMain()
        {
            Game1.WindowManager.GameState = Managers.GameState.MainMenu;
            Game1.WindowManager.UnloadGameplay();
            Game1.SongManager.Play("intro");
        }
    }
}
