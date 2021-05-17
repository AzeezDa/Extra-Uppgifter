using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace GameServer
{
    public static class GameplayHandler
    {
        public static Dictionary<string, Socket> PlayerSocketPairs = new Dictionary<string, Socket>();
        public static SortedList<string, int> PlayerHighScore = new SortedList<string, int>();

        public static KeyValuePair<string, Socket> Host;
        public static bool HostExists = false;

        public static void Handle(Socket socket, string request)
        {
            // The first three characters of the request is a command to help the handler know what to do
            string requestCommand = request.Substring(0, 3);

            // The rest is the "argument/s" of the command
            request = request[3..];

            switch (requestCommand)
            {
                // Local Game Host: LGH<player name>
                case "LGH":
                    PlayerHostRequest(request, socket);
                    break;

                // Local Game Join: LGJ<player name>
                case "LGJ":
                    PlayerJoinRequest(request, socket);
                    break;
                
                // Local Game Leave: LGL<player name>
                case "LGL":
                    PlayerLeave(request);
                    break;
                
                // Local Game Start: LGS
                case "LGS":
                    StartGame();
                    break;

                // Player Defeated Boss
                case "PDB":
                    PlayerDefeatedBoss(request);
                    break;

                // Player Defeated All
                case "PDA":
                    PlayerDefeatedAll(request);
                    break;
                default:
                    break;
            }
        }

        public static void Send(Socket socket, string request)
        {
            Program.Send(socket, "GME" + request);
        }

        public static void PlayerJoinRequest(string name, Socket socket)
        {
            if (PlayerSocketPairs.ContainsKey(name))
            {
                Send(socket, "PJR");
                return;
            }
            PlayerSocketPairs.Add(name, socket);
            PlayerHighScore.Add(name, 0);

            string players = string.Join(',', PlayerSocketPairs.Keys);
            Send(socket, $"PJA{name}|{players}");

            foreach (Socket s in PlayerSocketPairs.Values)
            {
                if (s == socket)
                    continue;

                Send(s, "LGJ" + name);
            }
        }

        public static void PlayerHostRequest(string name, Socket socket)
        {
            if (!HostExists)
            {
                Host = new KeyValuePair<string, Socket>(name, socket);

                Send(socket, "LHA" + name);

                foreach (Socket s in Program.ConnectedSockets)
                {
                    if (s == socket)
                        continue;

                    Send(s, "LGA");
                }
                HostExists = true;
                PlayerSocketPairs.Add(name, socket);
                PlayerHighScore.Add(name, 0);
                return;
            }
            Send(socket, "LHR");
        }

        public static void PlayerLeave(string name)
        {
            PlayerSocketPairs.Remove(name);
            PlayerHighScore.Remove(name);
            if (Host.Key == name)
            {
                if (PlayerSocketPairs.Count > 0)
                {
                    foreach (string playerName in PlayerSocketPairs.Keys)
                    {
                        Host = new KeyValuePair<string, Socket>(playerName, PlayerSocketPairs[playerName]);
                        return;
                    }
                }

                CloseLocalGame();
                HostExists = false;
            }
        }

        public static void CloseLocalGame()
        {
            foreach (Socket s in Program.ConnectedSockets)
            {
                Send(s, "LGC");
            }
        }

        public static void StartGame()
        {
            foreach (Socket s in PlayerSocketPairs.Values)
            {
                Send(s, "LGS");
            }
        }

        public static void SendHighScoreList()
        {
            StringBuilder highScore = new StringBuilder();
            foreach (KeyValuePair<string, int> highscoreItem in PlayerHighScore)
                highScore.Append($"{highscoreItem.Key}:{highscoreItem.Value},");
            highScore.Remove(highScore.Length - 1, 1);

            string highscoreList = "CHS" + highScore.ToString();

            foreach (Socket socket in PlayerSocketPairs.Values)
                Send(socket, highscoreList);
        }

        public static void PlayerDefeatedBoss(string player)
        {
            if (HostExists)
            {
                PlayerHighScore[player]++;
                foreach (Socket socket in PlayerSocketPairs.Values)
                    Send(socket, "PDB" + player);
            }
        }

        public static void PlayerDefeatedAll(string player)
        {
            foreach (Socket socket in PlayerSocketPairs.Values)
                Send(socket, "PDA" + player);

            PlayerSocketPairs = new Dictionary<string, Socket>();
            PlayerHighScore = new SortedList<string, int>();
            HostExists = false;
        }
    }
}
