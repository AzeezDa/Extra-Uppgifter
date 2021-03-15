using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Some_Knights_and_a_Dragon.Managers;

namespace Some_Knights_and_a_Dragon.Windows
{
    public class OnlinePrePlayWindow : MenuWindow
    {
        public bool LocalAvailable { get; set; }

        public string LocalPlayerName { get => ((TextBox)MenuItems["Name"]).Text; }
        public OnlinePrePlayWindow() : base("Online Window")
        {
            LocalAvailable = false;
            MenuItems.Add("Name", new TextBox(new Vector2(640, 400), "Name", backgroundText: "Enter Name"));
            MenuItems.Add("Create", new Button(new Vector2(640, 500), "Create Local", CreateLocalButton));
            MenuItems.Add("Join", new Button(new Vector2(640, 700), "Join Local", JoinLocalButton));
            MenuItems.Add("Return", new Button(new Vector2(640, 900), "Return", ReturnButton));
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            if (LocalAvailable)
                Game1.FontManager.WriteText(_spriteBatch, "Local Game Available!", new Vector2(640, 640), Color.LightGreen);
            else
                Game1.FontManager.WriteText(_spriteBatch, "No Local Game Available.", new Vector2(640, 640), Color.Red);
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }

        private void JoinLocalButton()
        {
            Game1.WindowManager.GameState = GameState.JoinedHost;
            NetworkClient.Send("PLJ" + LocalPlayerName);
        }

        private void CreateLocalButton()
        {
            if (NetworkClient.Connected && LocalPlayerName.Length > 0 && !LocalAvailable)
            {
                NetworkClient.Send($"LGC{LocalPlayerName}");
                Game1.WindowManager.GameState = GameState.OnlineHost;
            }
        }

        private void ReturnButton()
        {
            Game1.WindowManager.GameState = GameState.PrePlay;
        }
    }
}
