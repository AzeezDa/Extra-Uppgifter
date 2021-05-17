using System;
using System.Collections.Generic;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers.PlayerManagement
{
    // This class inherits from the HighScoreItem class because they share the same fields but this class is more about handling the recording of the values
    public class HighScoreRecorder : HighScoreItem
    {
        // Starts the recorder with the player name, 0 bossesdefeated and 0 ticks on the time
        public HighScoreRecorder(string name) : base (name, 0, TimeSpan.Zero)
        {
        }

        public void Start()
        {
            // Start the timer by storing the correct time in ticks
            Time = TimeSpan.FromTicks(DateTime.Now.Ticks);
        }

        public void Stop()
        {
            // Stop the timer and the elapsed time the is difference between current time and old time
            Time = TimeSpan.FromTicks(DateTime.Now.Ticks) - Time;
        }

        public void BossDefeatedUpdate()
        {
            // When a boss is defeated, add one to the bosses defeated
            BossesDefeated++;

            if (Networking.GameplayNetworkHandler.InLocalGame)
                Networking.GameplayNetworkHandler.Send("PDB" + Networking.GameplayNetworkHandler.Name);
        }

        public void SaveHighScore()
        {
            // Save the high score
            HighScoreManager.SaveHighScore(this);
        }
    }
}
