using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Some_Knights_and_a_Dragon.Managers;
using System.Diagnostics;

namespace Some_Knights_and_a_Dragon.Managers.Networking
{
    public static class GameplayNetworkHandler
    {
        public static bool LocalExists = false;
        public static bool JoinedLocal = false;
        public static bool IsHost = false;
        public static bool InLocalGame = false;
        public static string Name;

        public static List<string> Players = new List<string>();
        public static SortedList<string, int> PlayerHighscore = new SortedList<string, int>();

        public static void Send(string request)
        {
            NetworkClient.Send($"GME{request}");
        }

        public static void Handle(string fullRequest)
        {
            Debug.WriteLine(fullRequest);
            string command = fullRequest[0..3];
            string request = fullRequest[3..];
            Debug.WriteLine(command);
            Debug.WriteLine(request);
            switch (command)
            {
                case "PJA":
                    string[] requests = request.Split('|');
                    Name = requests[0];
                    Players.AddRange(requests[1].Split(','));
                    JoinedLocal = true;
                    Game1.WindowManager.GameState = GameState.LocalLobby;
                    break;
                case "LGA":
                    LocalExists = true;
                    break;
                case "LHA":
                    LocalExists = true;
                    IsHost = true;
                    Name = request;
                    JoinedLocal = true;
                    Players.Add(Name);
                    Game1.WindowManager.GameState = GameState.LocalLobby;
                    break;
                case "LGJ":
                    Players.Add(request);
                    break;
                case "LGC":
                    CloseLocalGame();
                    break;
                case "LGS":
                    StartGame();
                    break;
                case "PDB":
                    PlayerDefeatedBoss(request);
                    break;
                case "PDA":
                    PlayerDefeatedAll(request);
                    break;
                default:
                    break;
            }
        }

        private static void PlayerDefeatedAll(string request)
        {
            Game1.WindowManager.GameState = GameState.LocalEnd;
        }

        public static void StartGame()
        {
            if (JoinedLocal)
            {
                Game1.WindowManager.LoadGameplay();
                Game1.WindowManager.GetGameplayWindow().LoadFromSave("Fresh.start");
                InLocalGame = true;
                Game1.WindowManager.GameState = GameState.Playing;
                foreach (string player in Players)
                {
                    PlayerHighscore.Add(player, 0);
                }
            }
        }

        public static void CloseLocalGame()
        {
            Players = null;
            LocalExists = false;
            JoinedLocal = false;
            IsHost = false;
            InLocalGame = false;
            Players = new List<string>();
            Name = null;
            Game1.WindowManager.GameState = GameState.PrePlay;
        } 

        public static void Leave()
        {
            Send("LGL" + Name);
        }

        public static void PlayerDefeatedBoss(string player)
        {
            PlayerHighscore[player]++;
        }

        public static void Reset()
        {
            LocalExists = false;
            InLocalGame = false;
            JoinedLocal = false;
            IsHost = false;
            Name = null;
            Players = new List<string>();
            PlayerHighscore = new SortedList<string, int>();
            Game1.WindowManager.GameState = GameState.MainMenu;
        }
    }
}
