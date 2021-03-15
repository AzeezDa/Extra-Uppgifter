using System;
using System.Collections.Generic;
using System.Text;
using Some_Knights_and_a_Dragon.Entities;
using Some_Knights_and_a_Dragon.Entities.Creatures;
using Some_Knights_and_a_Dragon.Entities.Projectiles;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Some_Knights_and_a_Dragon.Managers.Networking
{
    public static class GameplayNetworkingHandler
    {
        private static Queue<string> RequestQueue = new Queue<string>(50);
        private static StringBuilder FullRequest = new StringBuilder();

        // Format: GME REQUEST|REQUEST|REQUEST

        // Adds a request to the queue to send to the server
        public static void QueueRequest(string request)
        {
            RequestQueue.Enqueue(request);
        }

        public static void SendRequests()
        {
            if (RequestQueue.Count > 0)
            {
                // Add the request command
                FullRequest.Append("GME");

                // Add all but last requests with the separator
                for (int i = 0; i < RequestQueue.Count - 1; i++)
                    FullRequest.Append(RequestQueue.Dequeue() + "|");

                // Add the last request
                FullRequest.Append(RequestQueue.Dequeue());

                // Send the request to the server
                NetworkClient.Send(FullRequest.ToString());

                FullRequest.Clear();
            }
        }

        public static void Handle(string fullRequest)
        {
            Debug.WriteLine(fullRequest);
            string[] requests = fullRequest[1..].Split('|');


            for (int i = 0; i < requests.Length; i++)
            {
                string command = requests[i][0..2];
                string request = requests[i][3..];

                Debug.WriteLine(command);
                Debug.WriteLine(request);

                switch (command)
                {
                    case "AV":
                        AddVelocity(request);
                        break;

                    case "PJ":
                        PlayerJoin(request);
                        break;

                    default:
                        break;
                }
            }
        }

        private static void PlayerJoin(string request)
        {
            string[] data = request.Split(':');
            Game1.WindowManager.GetGameplayWindow().ConnectNewPlayer(data[0], (Creature)Activator.CreateInstance(null, data[1]).Unwrap(), int.Parse(data[2]));
        }

        private static void NewEntity()
        {
            
        }

        private static void NewProjectile()
        {

        }

        private static void ChangeHealth()
        {

        }

        private static void ChangePosition(string request)
        {
            string[] requests = request.Split(':');
            string[] vectorData = requests[1].Split(',');
            Vector2 position = new Vector2(int.Parse(vectorData[0]), int.Parse(vectorData[1]));

            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures[int.Parse(requests[0])].ChangePosition(position);
        }

        private static void AddVelocity(string request)
        {
            string[] requests = request.Split(':');
            string[] vectorData = requests[1].Split(',');
            Vector2 position = new Vector2(int.Parse(vectorData[0]), int.Parse(vectorData[1]));

            Game1.WindowManager.GetGameplayWindow().CurrentLevel.Creatures[int.Parse(requests[0])].AddToVelocity(position);
        }
    }
}
