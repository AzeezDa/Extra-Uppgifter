using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Some_Knights_and_a_Dragon.Managers.Networking
{
    // This class handles requests sent from the server
    public static class RequestHandler
    {
        // Handles the request
        public static void Handle(string request)
        {
            // The first three characters of the request is a command to help the handler know what to do
            string requestCommand = request.Substring(0, 3);

            // The rest is the "argument/s" of the command
            request = request[3..];

            Debug.WriteLine(requestCommand);
            Debug.WriteLine(request);

            switch (requestCommand)
            {
                // The request is the high score data that is saved locally in the HighScore.xml
                case "HSF":
                    HighScoreManager.FetchHighScore(request);
                    break;

                // Handle gameplay requests
                case "GME":
                    GameplayNetworkingHandler.Handle(request);
                    break;

                // Sets up local when there is host
                case "LGC":
                    ((Windows.OnlinePrePlayWindow)Game1.WindowManager.Windows["Online Pre Play"]).LocalAvailable = true;
                    break;

                // Host left
                case "HLV":
                    ((Windows.OnlinePrePlayWindow)Game1.WindowManager.Windows["Online Pre Play"]).LocalAvailable = false;
                    break;

                case "PLJ":
                    
                    ((Windows.LocalHostWindow)Game1.WindowManager.Windows["Online Host"]).PlayersJoined.Add(request);
                    break;

                case "PLL":
                    ((Windows.LocalHostWindow)Game1.WindowManager.Windows["Online Host"]).PlayersJoined.Remove(request);
                    break;

                case "LGS":
                    if (Game1.WindowManager.GameState == GameState.JoinedHost)
                    {
                        Game1.WindowManager.LoadGameplay();
                        Game1.WindowManager.GetGameplayWindow().LoadFromRequest(request);
                    }
                    break;

                default:
                    break;
            }

        }
    }
}
