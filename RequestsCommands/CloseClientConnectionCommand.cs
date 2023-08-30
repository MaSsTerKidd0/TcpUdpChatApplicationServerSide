using Server.ChatObserver;
using Server.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.RequestsCommands
{
    public class CloseClientConnectionCommand<Reach> : ICommand<Reach>
    {
        public void Execute(string[] reqParts, Reach reach, Server<Reach> server)
        {
            if (reach is Socket socket)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Client<Reach> client = server.Clients.Find(c => c.ClientReach.Equals(socket));
                server.Clients.Remove(client);
                foreach (Chat<Reach> chat in server.Chats)
                {
                    chat.Detach(client);
                }    
            }
        }
    }
}
