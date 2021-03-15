using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Microsoft.Xna.Framework;
using Some_Knights_and_a_Dragon.Managers;

namespace Some_Knights_and_a_Dragon.Windows
{
    class OnlineJoinWindow : MenuWindow
    {
        public OnlineJoinWindow() : base("Online Join")
        {
            MenuItems.Add("Leave", new Button(new Vector2(640, 900), "Leave", LeaveButton));
        }

        private void LeaveButton()
        {
            NetworkClient.Send($"PLL{((OnlinePrePlayWindow)Game1.WindowManager.Windows["Online Pre Play"]).LocalPlayerName}");
            Game1.WindowManager.GameState = GameState.OnlinePrePlay;
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);
            Game1.FontManager.WriteText(_spriteBatch,
                $"Greetings {((OnlinePrePlayWindow)Game1.WindowManager.Windows["Online Pre Play"]).LocalPlayerName}! Wait for the host to start!",
                new Vector2(640, 500));
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }
    }
}
