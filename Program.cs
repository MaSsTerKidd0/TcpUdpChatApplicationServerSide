using Server.Protocols;
using System.Net.Sockets;

namespace Server
{
    public class Program
    {

        private static Dictionary<int, ServerInterface> protocols = new Dictionary<int, ServerInterface>() 
        {
            { 1, new TcpServer() },
            { 2, new UdpServer() }
        
        };

        public static void Main(string[] args)
        {
           
            ServerInterface server;

            Console.WriteLine("--Server--");
            server = protocols[ServerProtocolSelection()];
            server.ExecuteServer();
        }

        public static int ServerProtocolSelection()
        {
            int protocolChoice = 0;
            Console.WriteLine("1.Tcp Server");
            Console.WriteLine("2.Udp Server");
            Console.Write("Pls Enter Your Choice: ");
            while (!protocols.ContainsKey(protocolChoice))
            {
                int.TryParse(Console.ReadLine(), out protocolChoice);
            }

            return protocolChoice;
        }
    }

}