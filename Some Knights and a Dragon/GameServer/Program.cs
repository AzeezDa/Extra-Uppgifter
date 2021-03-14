using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace GameServer
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

    public class Program
    {
        // Socket for the server
        private static Socket ServerSocket;

        // Port used, an arbitrary available port is used
        private const int Port = 4301;

        // The Sockets connected to the server
        public static readonly List<Socket> ConnectedSockets = new List<Socket>();

        // Set up the buffer and its size
        private const int BufferSize = 2048;
        private static readonly byte[] Buffer = new byte[BufferSize];

        // Used in closing the server, to stop all connections
        private static bool Closing = false;

        static void Main()
        {
            // Set up the server
            SetUp();
            
            // Stop the thread
            Console.ReadLine();

            // Close all connections
            Closing = true;
            CloseAllSockets();
        }

        public static void SetUp()
        {
            try
            {
                // Create the server socket as a TCP socket
                ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Bind it to an endpoint
                ServerSocket.Bind(new IPEndPoint(IPAddress.Any, Port));

                // Listen for connections
                ServerSocket.Listen(5);

                // Begin accepting connections
                ServerSocket.BeginAccept(AcceptCallback, null);

                // Information
                Console.WriteLine("$ Server is Ready");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            // Close acceptance if the server is closing
            if (Closing)
                return;

            try
            {
                // Store the connecting socket
                Socket current = ServerSocket.EndAccept(ar);

                // Information to the user
                Console.WriteLine($"$ Connected with {current.RemoteEndPoint}.");

                // Add server to the socket list
                ConnectedSockets.Add(current);

                // Asynchronously receive data from the socket.
                current.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, current);

                // Continue accepting
                ServerSocket.BeginAccept(AcceptCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            // End receiving if server is closing
            if (Closing)
                return;

            // Create a ResponseState to pass into the async calls to retrieve all data if it is longer than the buffer
            ResponeState state = new ResponeState();
            state.Socket = (Socket)ar.AsyncState;

            // The amount of data received
            int received;

            try
            {
                // Receive the data and store the amount
                received = state.Socket.EndReceive(ar);

                // Get the current response
                string response = Encoding.UTF8.GetString(Buffer);

                // The length of data is the first thing received, it is parsed and stored in the ResponseState
                string length = response.Split(' ')[0];
                state.RemainingBytes = int.Parse(length);

                // Append the data received so far
                state.Response.Append(Encoding.UTF8.GetString(Buffer).Substring(length.Length + 1, received - length.Length - 1));

                // Reduce the received data from the remaining
                state.RemainingBytes -= received;

                // Information
                Console.WriteLine($"$ Request from {state.Socket.RemoteEndPoint}: {state.Response}");

                // If there is still data left receive more and call the callback to handle more data
                if (state.RemainingBytes > 0)
                {
                    state.Socket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ContinueReceiveCallback, state);
                }
                else
                {
                    // If the data received is EXT, then close the socket and shut it down and close the receiving loop
                    if (state.Response.ToString() == "EXT")
                    {
                        state.Socket.Shutdown(SocketShutdown.Both);
                        state.Socket.Close();
                        ConnectedSockets.Remove(state.Socket);
                        return;
                    }

                    // Continue receiving
                    state.Socket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, state.Socket);
                    
                    // Handle the request sent from the client
                    RequestHandler.Handle(state.Response.ToString(), state.Socket);
                }
            }
            catch (SocketException)
            {
                // If a socket exception is encountered then close the socket, shut it down and remove it.
                Console.WriteLine($"$ Connection with {state.Socket.RemoteEndPoint} encountered an error.");
                state.Socket.Shutdown(SocketShutdown.Both);
                state.Socket.Close();
                ConnectedSockets.Remove(state.Socket);
                return;
            }
            catch (Exception)
            {
                // If any exception is encountered then close the socket, shut it down and remove it.
                Console.WriteLine($"$ Connection with {state.Socket.RemoteEndPoint} encountered an error.");
                state.Socket.Shutdown(SocketShutdown.Both);
                state.Socket.Close();
                ConnectedSockets.Remove(state.Socket);
                return;
            }

        }

        private static void ContinueReceiveCallback(IAsyncResult ar)
        {
            // Store the response state from the receive callback to continue receiving.
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
                    state.Socket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ContinueReceiveCallback, state);
                }
                else
                {
                    // Else recieve for a new request
                    state.Socket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, state.Socket);

                    // Handle the request.
                    RequestHandler.Handle(state.Response.ToString(), state.Socket);
                }

                // Information
                Console.WriteLine($"$ Request from {state.Socket.RemoteEndPoint}: {state.Response}");
            }
            catch (SocketException)
            {
                // If a socket exception is encountered then close the socket, shut it down and remove it.
                Console.WriteLine($"$ Connection with {state.Socket.RemoteEndPoint} encountered an error.");
                state.Socket.Shutdown(SocketShutdown.Both);
                state.Socket.Close();
                ConnectedSockets.Remove(state.Socket);
                return;
            }
            catch (Exception)
            {
                // If any exception is encountered then close the socket, shut it down and remove it.
                Console.WriteLine($"$ Connection with {state.Socket.RemoteEndPoint} encountered an error.");
                state.Socket.Shutdown(SocketShutdown.Both);
                state.Socket.Close();
                ConnectedSockets.Remove(state.Socket);
                return;
            }
        }

        public static void Send(Socket socket, string text)
        {
            try
            {
                // When sending data to the socket, add the length of the data at the start of the message to let the receiever know how big the message is
                // This help to not overflow the buffer and receive the data as a whole
                text = $"{text.Length} {text}";

                // Asynchronously send the data
                socket.BeginSend(Encoding.UTF8.GetBytes(text), 0, text.Length, SocketFlags.None, SendCallback, socket);
            }
            catch (SocketException)
            {
                // If a socket exception is encountered then close the socket, shut it down and remove it.
                Console.WriteLine($"$ Connection with {socket.RemoteEndPoint} encountered an error.");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                ConnectedSockets.Remove(socket);
                return;
            }
            catch (Exception)
            {
                // If any exception is encountered then close the socket, shut it down and remove it.
                Console.WriteLine($"$ Failed to send to {socket.RemoteEndPoint}.");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                ConnectedSockets.Remove(socket);
                return;
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

                // Information
                Console.WriteLine($"$ Sent to {socket.RemoteEndPoint}.");
            }
            catch (Exception)
            {
                // If any exception is encountered then close the socket, shut it down and remove it.
                Console.WriteLine($"$ Failed to send to {socket.RemoteEndPoint}.");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                ConnectedSockets.Remove(socket);
            }
        }

        private static void CloseAllSockets()
        {
            // Closes all sockets in the server
            foreach (Socket socket in ConnectedSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            ServerSocket.Close();
        }
    }
}
