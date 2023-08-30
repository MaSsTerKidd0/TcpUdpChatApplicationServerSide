using Server.Client;
using System.Net;
using System;
using System.Net.Sockets;
using System.Text;

namespace Server.Protocols
{
    public class TcpServer : Server<Socket>
    {
        public TcpServer() : base()
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _endPoint.Port = 4320;
        }

        protected override void InitConnection()
        {
            _listener.Bind(_endPoint);
            _listener.Listen(1000);
        }

        protected override void ServerStart()
        {
            //TODO: Change to Flag isRunning
            while (true)
            {
                Socket socket = _listener.AcceptAsync().Result;
                Task task = Task.Run(() => { Listen(socket); });
            }
        }

        private void Listen(Socket socket)
        {
            string data = string.Empty;

            while (true)
            {
                data = ReceiveMsg(socket);
                Console.WriteLine("Client Request -> " + data);
                HandleRequest(data, socket);
                if (!socket.Connected)
                    break;
            }
            Console.WriteLine("Out");
        }

        protected override void Send(string msg, Client<Socket> client)
        {
            byte[] messageSent = Encoding.ASCII.GetBytes(msg);
            client.ClientReach.Send(messageSent, SocketFlags.None);
        }

        private string ReceiveMsg(Socket socket)
        {
            byte[] lengthBuffer = new byte[4];
            int bytesRead = socket.Receive(lengthBuffer, sizeof(int), SocketFlags.None);
            if (bytesRead != sizeof(int))
            {
                return null;
            }

            int messageLength = BitConverter.ToInt32(lengthBuffer, 0);
            byte[] messageBuffer = new byte[messageLength];
            int totalBytesRead = 0;

            while (totalBytesRead < messageLength)
            {
                int bytesToRead = Math.Min(messageLength - totalBytesRead, messageBuffer.Length);
                int bytesReadNow = socket.Receive(messageBuffer, totalBytesRead, bytesToRead, SocketFlags.None);

                if (bytesReadNow <= 0)
                {
                    return null;
                }

                totalBytesRead += bytesReadNow;
            }
            return Encoding.ASCII.GetString(messageBuffer, 0, totalBytesRead);
        }
    }
}

