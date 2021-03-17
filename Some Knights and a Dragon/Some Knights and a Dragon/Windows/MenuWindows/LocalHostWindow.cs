using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Windows.Menus;
using Some_Knights_and_a_Dragon.Managers;
using System.Diagnostics;
using Some_Knights_and_a_Dragon.Entities.Creatures;

namespace Some_Knights_and_a_Dragon.Windows
{
    class LocalHostWindow : MenuWindow
    {
        public List<string> PlayersJoined { get; private set; }
        private string HostName { get => ((OnlinePrePlayWindow)Game1.WindowManager.Windows["Online Pre Play"]).LocalPlayerName; }
        public LocalHostWindow() : base("Local Host")
        {
            MenuItems.Add("Leave", new Button(new Vector2(340, 900), "Leave", LeaveButton));
            MenuItems.Add("Start", new Button(new Vector2(940, 900), "Start", StartButton));
            PlayersJoined = new List<string>();
        }

        private void StartButton()
        {
            Game1.WindowManager.LoadGameplay();
            Game1.WindowManager.GetGameplayWindow().LoadFromSave("Fresh.start");

            StringBuilder startRequest = new StringBuilder();

            startRequest.Append($"L:Fresh.start|");

            foreach (string playerName in PlayersJoined)
            {
                Creature playerCreature = new Elf();
                int id = Game1.WindowManager.GetGameplayWindow().CurrentLevel.AddCreature(playerCreature);

                // Format: P:Name:CreatureType:CreatureID
                startRequest.Append($"P:{playerName}:{playerCreature.GetType()}:{id}|");

                Game1.WindowManager.GetGameplayWindow().ConnectNewPlayer(playerName, id);
            }

            Creature hostCreature = Game1.WindowManager.GetGameplayWindow().Player.Creature;
            startRequest.Append($"P:{HostName}:{hostCreature.GetType()}:{hostCreature.ID}");

            NetworkClient.Send("LGS" + startRequest.ToString());
            Game1.WindowManager.GameState = GameState.Playing;
            Game1.WindowManager.GetGameplayWindow().Online = true;
        }

        private void LeaveButton()
        {
            NetworkClient.Send("HLV" + HostName);
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
