using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Net;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class TryConnectWindow : MenuWindow
    {
        public TryConnectWindow() : base("TryConnectWindow")
        {
            // IP Address input
            MenuItems.Add("IP Address", new Menus.TextBox(new Vector2(640, 600), "IP Address", "192.168.0.", "IP Address"));

            // Try to initiate connection
            MenuItems.Add("Try Connect", new Menus.Button(new Vector2(640, 800), "Connect", TryConnect));

            // Return to main menu
            MenuItems.Add("Main Menu", new Menus.Button(new Vector2(640, 900), "Main Menu", MainMenuButton));
        }

        private void MainMenuButton()
        {
            Game1.WindowManager.GameState = Managers.GameState.MainMenu;
        }

        private void TryConnect()
        {
            // Create new endpoint with the provided IP Address
            Managers.NetworkClient.EndPoint = new IPEndPoint(IPAddress.Parse(((Menus.TextBox)MenuItems["IP Address"]).Text), Managers.NetworkClient.Port);

            // Initiate connection with the server
            Managers.NetworkClient.Setup();
        }
    }
}
