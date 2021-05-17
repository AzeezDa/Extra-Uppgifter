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

            //The <> brackets are not sent in the request, they are just used in the comments for clarity
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

                // Player Defeated Boss: PDB<player name>
                case "PDB":
                    PlayerDefeatedBoss(request);
                    break;

                // Player Defeated All: PDA<player name>
                case "PDA":
                    PlayerDefeatedAll(request);
                    break;
                default:
                    break;
            }
        }

        public static void Send(Socket socket, string request) // Sends a game request to a socket
        {
            Program.Send(socket, "GME" + request);
        }

        public static void PlayerJoinRequest(string name, Socket socket)
        {
            // Check if name already exists then reject the requst
            if (PlayerSocketPairs.ContainsKey(name))
            {
                Send(socket, "PJR"); // Player Join Rejected
                return;
            }
            
            // If success then add the name to the socket Dictionary and the highscore to the Sorted List
            PlayerSocketPairs.Add(name, socket);
            PlayerHighScore.Add(name, 0);

            // Send the current players in the game to the newly joined player
            string players = string.Join(',', PlayerSocketPairs.Keys); // format the names into "playername1,playername2,playername3" and so on
            Send(socket, $"PJA{name}|{players}"); // Player Join Accepted: PJA<player joined name>|<player1>,<player2>

            // Send the name of the player that joined to the other players
            foreach (Socket s in PlayerSocketPairs.Values)
            {
                if (s == socket)
                    continue;

                Send(s, "LGJ" + name); // Local Game Join: LGJ<playername>
            }
        }

        public static void PlayerHostRequest(string name, Socket socket) // When a player wants to be a host
        {
            if (!HostExists) // Checks if there is no host already
            {
                Host = new KeyValuePair<string, Socket>(name, socket); // Create the host Key Pair Value to store in the dictionary

                Send(socket, "LHA" + name); // Local Host Accepted: LHA<playername>

                // Send to all other player connected to the server
                foreach (Socket s in Program.ConnectedSockets)
                {
                    if (s == socket)
                        continue;

                    Send(s, "LGA"); // Local Game Available
                }
                HostExists = true; // Host is available
                PlayerSocketPairs.Add(name, socket); // Add the host to the list
                PlayerHighScore.Add(name, 0); // Add the host's highscore to the Sorted List
                return;
            }
            Send(socket, "LHR"); // Highscore Rejected
        }

        public static void PlayerLeave(string name)
        {
            // Removes the player from the dictionary and list
            PlayerSocketPairs.Remove(name);
            PlayerHighScore.Remove(name);

            // Change host if the host left
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

                // If no players left, close the local game and reset
                CloseLocalGame();
                HostExists = false;
            }
        }

        public static void CloseLocalGame()
        {
            // Send the close game request to all sockets
            foreach (Socket s in Program.ConnectedSockets)
            {
                Send(s, "LGC"); // Local Game Close
            }
        }

        public static void StartGame()
        {
            // Send to all in local game
            foreach (Socket s in PlayerSocketPairs.Values)
            {
                Send(s, "LGS"); // Local Game Start
            }
        }

        public static void SendHighScoreList()
        {
            // String builder to build the string from the Sorted List
            StringBuilder highScore = new StringBuilder();

            // Append the values in this format "playername:score,playername:score,playername:score"
            foreach (KeyValuePair<string, int> highscoreItem in PlayerHighScore)
                highScore.Append($"{highscoreItem.Key}:{highscoreItem.Value},");
            highScore.Remove(highScore.Length - 1, 1); // Remove last character which is a comma

            string highscoreList = "CHS" + highScore.ToString(); // Current High Score: CHSplayername:score,playername:score,playername:score

            // Send To all players in the local game
            foreach (Socket socket in PlayerSocketPairs.Values)
                Send(socket, highscoreList);
        }

        public static void PlayerDefeatedBoss(string player)
        {
            // Check to avoid conflict when ending the game (concurrent request handling can cause errors)
            if (HostExists)
            {
                // Increase the score for the player
                PlayerHighScore[player]++;

                // Send to players that the player defeated a boss
                foreach (Socket socket in PlayerSocketPairs.Values)
                    Send(socket, "PDB" + player); // Player Defeated Boss: PDB<playername>
            }
        }

        public static void PlayerDefeatedAll(string player)
        {
            // Send to all players that the player has defeated all the bosses
            foreach (Socket socket in PlayerSocketPairs.Values)
                Send(socket, "PDA" + player); // Player Defeated All: PDA<playername>

            // Reset the Dictionary, Sorted List and HostExists bool
            PlayerSocketPairs = new Dictionary<string, Socket>();
            PlayerHighScore = new SortedList<string, int>();
            HostExists = false;
        }
    }
}
