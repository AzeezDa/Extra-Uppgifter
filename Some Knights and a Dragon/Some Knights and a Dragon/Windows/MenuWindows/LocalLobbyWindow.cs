using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Some_Knights_and_a_Dragon.Managers.Networking;


namespace Some_Knights_and_a_Dragon.Windows
{
    public class LocalLobbyWindow : MenuWindow
    {
        Button HostStart;
        public LocalLobbyWindow() : base("Local Lobby")
        {
            HostStart = new Button(new Vector2(640, 850), "Start", startButtonClick);
            MenuItems.Add("Main Menu", new Button(new Vector2(640, 950), "Main Menu", mainMenuButton));
        }

        private void mainMenuButton()
        {
            if (GameplayNetworkHandler.IsHost)
            {
                GameplayNetworkHandler.Send("LHL");
                GameplayNetworkHandler.IsHost = false;
                GameplayNetworkHandler.JoinedLocal = false;
            }
        }

        private void startButtonClick()
        {
            GameplayNetworkHandler.Send("LGS");
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            if (GameplayNetworkHandler.IsHost)
                HostStart.Draw(_spriteBatch);

            Game1.FontManager.WriteTitle(_spriteBatch, "Players Joined", new Vector2(640, 100), Color.White);
            for (int i = 0; i < GameplayNetworkHandler.Players.Count; i++)
                Game1.FontManager.WriteText(_spriteBatch, GameplayNetworkHandler.Players[i], new Vector2(640, 150 + i * 50), Color.White);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
            if (GameplayNetworkHandler.IsHost)
                HostStart.Update();
        }
    }
}
