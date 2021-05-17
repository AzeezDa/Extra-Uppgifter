using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Managers.Networking;
using Some_Knights_and_a_Dragon.Windows.Menus;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class LocalGameEndWindow : MenuWindow
    {

        public LocalGameEndWindow() : base("Local Game End")
        {
            MenuItems.Add("Main Menu", new Button(new Vector2(640, 900), "Main Menu", MainMenuButton));
        }

        private void MainMenuButton()
        {
            Game1.WindowManager.GameState = Managers.GameState.MainMenu;
            GameplayNetworkHandler.Reset();
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            int i = 1;
            string player;
            foreach (KeyValuePair<string, int> score in GameplayNetworkHandler.PlayerHighscore)
            {
                if (i == 1)
                    player = $"Winner: {score.Key}";
                else
                    player = $"{i}. {score.Key}";

                Game1.FontManager.WriteText(_spriteBatch, player, new Vector2(640, 150 + i * 50), Color.White);

                if (i++ > 5)
                    break;
            }
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }
    }
}
