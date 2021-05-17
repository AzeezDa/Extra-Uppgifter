using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Some_Knights_and_a_Dragon.Managers.Networking;

namespace Some_Knights_and_a_Dragon.Managers
{
    public class ResponeState
    {
        // Bytes remaining to be received from the socket
        public int RemainingBytes = 0;

        // The current response received
        public StringBuilder Response = new StringBuilder();

        // The socket the data is received from
        public Socket Socket = null;
    }

    public static class NetworkClient
    {
        // Set up the buffer
        private const int BufferSize = 2048;
        private static readonly byte[] Buffer = new byte[BufferSize];


        // Port used for communication
        private const int Port = 4301;

        // The client socket and server address
        private static Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static IPEndPoint EndPoint = new IPEndPoint(IPAddress.Loopback, Port);

        // Timer to occasionally check if there is a connection and if there is not, try to connect
        private static Timer connectionCheckTimer = new Timer(20000);
        private static Timer tryConnectTimer = new Timer(60000);

        // True if socket is connected
        public static bool Connected { get => ClientSocket.Connected; }

        
        // The message the client sends to the server to check connection and tell the server the socket is "alive"
        private static readonly byte[] PingMessage = Encoding.UTF8.GetBytes("3 PNG");

        public static void Setup()
        {
            try
            {
                // Try to asynchronously connect to the server
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ClientSocket.BeginConnect(EndPoint, ConnectCallback, ClientSocket);
            }
            catch (SocketException)
            {
                CloseSocket();
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Store the socket
                Socket socket = (Socket)ar.AsyncState;

                // End the connection attempt
                socket.EndConnect(ar);

                // Begin receiving from the server
                Receive();
            }
            catch (SocketException) // Close the socket upon errors
            {
                CloseSocket();
            }
            
        }

        public static void Receive()
        {
            // Create a ResponseState to pass into the async calls to retrieve all data if it is longer than the buffer
            ResponeState state = new ResponeState();

            try
            {
                // Store the client socket in the state socket
                state.Socket = ClientSocket;

                // Begin asynchronously receiving
                ClientSocket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, state);
            }
            catch (SocketException) // Close socket upon errors
            {
                CloseSocket();
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            // Get the resposne state from the AsyncState
            ResponeState state = (ResponeState)ar.AsyncState;

            // The amount of bytes received
            int received;

            try
            {
                // Receive the data and store the amount of bytes received
                received = state.Socket.EndReceive(ar);

                if (received == 0) // If no data then listen
                {
                    Receive();
                    return;
                }

                // Convert the response from bytes to string
                string response = Encoding.UTF8.GetString(Buffer);

                // The length of data is the first thing received, it is parsed and stored in the ResponseState
                string length = response.Split(' ')[0];
                state.RemainingBytes = int.Parse(length);

                // Append the data received so far
                state.Response.Append(Encoding.UTF8.GetString(Buffer).Substring(length.Length + 1, received - length.Length - 1));

                // Reduce the received data from the remaining
                state.RemainingBytes -= received;

                // If there is still data left receive more and call the callback to handle more data
                if (state.RemainingBytes > 0)
                {
                    ClientSocket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ContinueReceiveCallback, state);
                }
                else
                {
                    // Else receive a new request
                    Receive();

                    // Handle the request
                    RequestHandler.Handle(state.Response.ToString());
                }
            }
            catch (SocketException) // Close sockets upon errors
            {
                CloseSocket();
            }
        }
        private static void ContinueReceiveCallback(IAsyncResult ar)
        {
            // Store the response state from the receive callback to continue receiving
            ResponeState state = (ResponeState)ar.AsyncState;

            // Amount of bytes received
            int received;

            try
            {
                // Receive the data and store the amount of bytes
                received = state.Socket.EndReceive(ar);

                // Store the response as a string after conversion from bytes to string
                string response = Encoding.UTF8.GetString(Buffer);

                // Add the string to the response state response string builder
                state.Response.Append(Encoding.UTF8.GetString(Buffer).Substring(0, received));

                // Reduce the amount of bytes remaining by the amount received
                state.RemainingBytes -= received;

                // If there is still data left then continue receiving with the ContinueReceiveCallback
                if (state.RemainingBytes > 0)
                {
                    ClientSocket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ContinueReceiveCallback, state);
                }
                else
                {
                    // Else receive a new request
                    Receive();

                    // Handle the request
                    RequestHandler.Handle(state.Response.ToString());
                }
            }
            catch (SocketException) // Close socket upon errors
            {
                CloseSocket();
            }
        }

        public static void Send(string data)
        {
            try
            {
                // When sending data to the socket, add the length of the data at the start of the message to let the receiever know how big the message is
                // This help to not overflow the buffer and receive the data as a whole
                data = $"{data.Length} {data}";

                // Asynchronously send the data
                ClientSocket.BeginSend(Encoding.UTF8.GetBytes(data), 0, data.Length, SocketFlags.None, SendCallback, ClientSocket);
            }
            catch (SocketException) // Close sockets upon errors
            {
                CloseSocket();
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            // Get the socket from the AsyncState
            Socket socket = (Socket)ar.AsyncState;

            try
            {
                // Finish sending
                socket.EndSend(ar);
            }
            catch (SocketException) // Close sockets upon errors
            {
                CloseSocket();
            }
        }


        public static void CheckConnection(GameTime gameTime)
        {
            // Check the timers for both connection "alive" check and try to connection check
            connectionCheckTimer.CheckTimer(gameTime);
            tryConnectTimer.CheckTimer(gameTime);

            // If connected and it's time to ping the server then do
            if (connectionCheckTimer.TimerOn && Connected)
            {
                try
                {
                    // Send the ping message
                    ClientSocket.BeginSend(PingMessage, 0, 5, SocketFlags.None, SendCallback, ClientSocket);
                }
                catch (SocketException) // Close socket upon errors
                {
                    CloseSocket();
                }
            }

            // If not connected and time to try to connect then do so
            if (tryConnectTimer.TimerOn && !Connected)
            {
                Setup();
            }
        }

        // Normally shut down the socket
        public static void Shutdown()
        {
            // If connected, send the exit message to the server and close the socket
            if (Connected)
            {
                Send("EXT");
                CloseSocket();
            }
        }

        public static void CloseSocket()
        {
            // Close the socket and shut it down
            try
            {
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
            }
            catch (SocketException) {}
        }
    }
}
