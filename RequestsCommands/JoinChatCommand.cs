using Server.Client;
using Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RequestsCommands
{
    public class JoinChatCommand<Reach> : ICommand<Reach>
    {
        public void Execute(string[] reqParts, Reach reach, Server<Reach> server)
        {
            string chatName = reqParts[0];
            IResponseBuilder responseBuilder = new JoinedGroupResponseBuilder(chatName);
            Client<Reach> joinedClient = server.Clients.Find(c => c.ClientReach.Equals(reach));
            server.Chats.Find(c => c.Info.Name == chatName).Attach(joinedClient);
            server.ServerSend(responseBuilder.BuildResponse(), joinedClient);
        }
    }
}
