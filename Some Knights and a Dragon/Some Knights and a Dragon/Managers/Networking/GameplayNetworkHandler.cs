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
        public static bool LocalExists = false; // Local game exists
        public static bool JoinedLocal = false; // Player joined a local game
        public static bool IsHost = false; // Player is host
        public static bool InLocalGame = false; // Player in the local game
        public static string Name; // Name of the player

        // List of all players in the game
        public static List<string> Players = new List<string>();

        // Sorted list of the highscore
        public static SortedList<string, int> PlayerHighscore = new SortedList<string, int>();

        public static void Send(string request)
        {
            NetworkClient.Send($"GME{request}"); // Send a game request
        }

        public static void Handle(string fullRequest)
        {
            // Cut the request into the first three characters (the identifier) and the rest of the request
            string command = fullRequest[0..3];
            string request = fullRequest[3..];

            // Handle command based on identeifier (first 3 letters)
            // The <> brackets are just used in the comments, they are just for clarity and are not in the actual requests
            switch (command)
            {
                case "PJA": // Player Join Accepted: PJA<currentPlayerName>|<otherPlayer1>,<otherPlayer2>
                    string[] requests = request.Split('|'); // Split with |
                    Name = requests[0]; // first name is player name
                    Players.AddRange(requests[1].Split(',')); // Get other player names and store them
                    JoinedLocal = true; // Change JoinedLocal bool to true because the player has joined
                    Game1.WindowManager.GameState = GameState.LocalLobby; // Change the window to the waiting lobby
                    break;
                case "LGA": // Local Game Available
                    LocalExists = true;
                    break;
                case "LHA": // Local Host Accepted
                    LocalExists = true; // Local game available
                    IsHost = true; // Player is the host
                    Name = request; // Set up player name
                    JoinedLocal = true; // Player is in local game
                    Players.Add(Name); // Add the name to the list
                    Game1.WindowManager.GameState = GameState.LocalLobby; // Change window to waiting lobby
                    break;
                case "LGJ": // Local Game Join: LGJ<playerName>
                    Players.Add(request); // Add player to the player list
                    break;
                case "LGC": // Local Game Closed
                    CloseLocalGame();
                    break;
                case "LGS": // Local Game Start
                    StartGame();
                    break;
                case "PDB": // Player Defeated Boss: PDB<playerName>
                    PlayerDefeatedBoss(request);
                    break;
                case "PDA": // Player Defeated All: PDA<playerName>
                    PlayerDefeatedAll();
                    break;
                default:
                    break;
            }
        }

        private static void PlayerDefeatedAll()
        {
            // If player defeated all, switch to end window that displays leaderboard
            Game1.WindowManager.GameState = GameState.LocalEnd;
        }

        public static void StartGame()
        {
            if (JoinedLocal) // If in game
            {
                // Load gameplay content
                Game1.WindowManager.LoadGameplay();

                // Load a fresh start
                Game1.WindowManager.GetGameplayWindow().LoadFromSave("Fresh.start");

                // Change bool to true
                InLocalGame = true;

                // Change to GamePlay window
                Game1.WindowManager.GameState = GameState.Playing;

                // Set highscore to 0 for all players
                foreach (string player in Players)
                {
                    PlayerHighscore.Add(player, 0);
                }
            }
        }

        public static void CloseLocalGame()
        {
            // Reset all values and return to preplay window
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
            Send("LGL" + Name); // Local Game Leave: LGL<playerName>
        }

        public static void PlayerDefeatedBoss(string player)
        {
            // Increase player's score
            PlayerHighscore[player]++;
        }

        public static void Reset()
        {
            // Reset all values and return to main menu
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
