using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Some_Knights_and_a_Dragon.Managers;
using System.Diagnostics;

namespace Some_Knights_and_a_Dragon.Windows
{
    class LocalHostWindow : MenuWindow
    {
        public List<string> PlayersJoined { get; private set; }
        public LocalHostWindow() : base("Local Host")
        {
            MenuItems.Add("Leave", new Button(new Vector2(340, 900), "Leave", LeaveButton));
            MenuItems.Add("Start", new Button(new Vector2(940, 900), "Start", StartButton));
            PlayersJoined = new List<string>();
        }

        private void StartButton()
        {
            throw new NotImplementedException();
        }

        private void LeaveButton()
        {
            NetworkClient.Send("HLV" + ((OnlinePrePlayWindow)Game1.WindowManager.Windows["Online Pre Play"]).LocalPlayerName);
            Game1.WindowManager.GameState = GameState.OnlinePrePlay;
            PlayersJoined.Clear();
        }

        public override void Draw(ref SpriteBatch _spriteBatch)
        {
            base.Draw(ref _spriteBatch);

            Game1.FontManager.WriteTitle(_spriteBatch, "Players Joined", new Vector2(640, 150));

            for (int i = 0; i < PlayersJoined.Count; i++)
            {
                Game1.FontManager.WriteText(_spriteBatch, PlayersJoined[i], new Vector2(640, 250 + i * 50));
            }
        }

        public override void Update(ref GameTime gameTime)
        {
            base.Update(ref gameTime);
        }
    }
}
