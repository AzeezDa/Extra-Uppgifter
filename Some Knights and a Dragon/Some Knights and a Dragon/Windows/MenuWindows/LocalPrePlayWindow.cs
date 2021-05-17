using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Some_Knights_and_a_Dragon.Managers;
using Some_Knights_and_a_Dragon.Managers.Networking;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class LocalPrePlayWindow : MenuWindow
    {
        public LocalPrePlayWindow() : base("LocalPrePlayWindow")
        {
            MenuItems.Add("Name", new TextBox(new Vector2(640, 500), "Name", backgroundText: "Name"));
            MenuItems.Add("Create Local", new Button(new Vector2(640, 800), "Create Local", CreateLocalButton));
            MenuItems.Add("Join Local", new Button(new Vector2(640, 900), "Join Local", JoinLocalButton));
        }

        private void JoinLocalButton()
        {
            // Check if connected, and if there is a name and if there is a local game
            if (NetworkClient.Connected && ((TextBox)MenuItems["Name"]).Text.Length > 0 && GameplayNetworkHandler.LocalExists)
            {
                GameplayNetworkHandler.Send("LGJ" + ((TextBox)MenuItems["Name"]).Text); // Send Local Game Join request: LGJ<playerName>
            }
        }

        private void CreateLocalButton()
        {
            // Check if connected, there is a name and if there local exists
            if (NetworkClient.Connected && ((TextBox)MenuItems["Name"]).Text.Length > 0 && !GameplayNetworkHandler.LocalExists)
            {
                GameplayNetworkHandler.Send("LGH" + ((TextBox)MenuItems["Name"]).Text); // Request to become host by: Local Game Host: LGH<playerName>
            }
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            // Write informative text is the player exists
            if (GameplayNetworkHandler.LocalExists)
                Game1.FontManager.WriteText(_spriteBatch, "Local Game Available!", new Vector2(640, 600), Color.Green);
            else
                Game1.FontManager.WriteText(_spriteBatch, "Local Game Unavailable!", new Vector2(640, 600), Color.Red);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }
    }
}
