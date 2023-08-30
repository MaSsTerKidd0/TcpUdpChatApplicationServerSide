using Server.Client;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Protocols
{
    public class UdpServer : Server<EndPoint>
    {
        public UdpServer() : base()
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _endPoint.Port = 1690;
        }

        protected override void InitConnection()
        {
            _listener.Bind(_endPoint);
        }
        protected override void ServerStart()
        {
            RecieveMessage();
        }

        private void RecieveMessage()
        {
            string res;
            int resSize = 0;
            byte[] buffer = new byte[1024];
            EndPoint ep = _endPoint;

            while (true)
            {
                resSize = _listener.ReceiveFrom(buffer, buffer.Length, SocketFlags.None, ref ep);
                res = Encoding.UTF8.GetString(buffer, 0, resSize);
                Console.WriteLine("Client Msg -> " + res);
                HandleRequest(res, ep);
            }
        }

        protected override void Send(string msg, Client<EndPoint> client)
        {
             _listener.SendTo(Encoding.ASCII.GetBytes(msg), client.ClientReach);
        }
    }
}
