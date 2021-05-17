using System;
using System.Collections.Generic;
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

            switch (requestCommand)
            {
                // The request is the high score data that is saved locally in the HighScore.xml
                case "HSF":
                    HighScoreManager.FetchHighScore(request);
                    break;

                case "GME": // Handle game requests
                    GameplayNetworkHandler.Handle(request);
                    break;
                default:
                    break;
            }

        }
    }
}
