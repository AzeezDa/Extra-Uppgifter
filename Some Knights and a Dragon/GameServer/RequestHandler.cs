using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace GameServer
{
    // This class handles requests sent from the server
    public static class RequestHandler
    {

        public static void Handle(string request, Socket socket)
        {
            // The first three characters of the request is a command to help the handler know what to do
            string requestCommand = request.Substring(0, 3);

            // The rest is the "argument/s" of the command
            request = request[3..];

            switch (requestCommand)
            {
                // Handles a new highscore sent from the client
                case "NHS":
                    // Split from name,bosses,time into string array.
                    string[] highscoreData = request.Split(',');
                    Some_Knights_and_a_Dragon.Managers.HighScoreItem highScoreItem =
                        new Some_Knights_and_a_Dragon.Managers.HighScoreItem(highscoreData[0], int.Parse(highscoreData[1]), TimeSpan.FromTicks(long.Parse(highscoreData[2])));
                    Some_Knights_and_a_Dragon.Managers.HighScoreManager.SaveHighScore(highScoreItem);
                    break;

                // Sends the highscore file to the requester
                case "HSF":
                    using (StreamReader r = new StreamReader("../../../Data/HighScore.xml"))
                    {
                        string tosend = "HSF" + r.ReadToEnd();
                        Program.Send(socket, tosend);
                    }
                    break;

                case "GME":
                    foreach (Socket client in Program.ConnectedSockets)
                    {
                        if (client == socket)
                            continue;
                        Program.Send(client, "GME" + request);
                    }
                    break;

                case "LGC":
                    SendToAllExcept(socket, "LGC" + request);
                    break;

                case "HLV":
                    SendToAllExcept(socket, "HLV" + request);
                    break;

                case "PLJ":
                    SendToAllExcept(socket, "PLJ" + request);
                    break;

                case "PLL":
                    SendToAllExcept(socket, "PLL" + request);
                    break;

                case "LGS":
                    SendToAllExcept(socket, "LGS" + request);
                    break;

                default:
                    break;
            }
        }

        private static void SendToAllExcept(Socket socket, string request)
        {
            foreach (Socket client in Program.ConnectedSockets)
            {
                if (client == socket)
                    continue;
                Program.Send(client, request);
            }
        }
    }
}
