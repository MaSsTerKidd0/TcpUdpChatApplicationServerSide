using Server.Client;
using Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Server.RequestsCommands
{
    public class ClientRegisterCommand<Reach> : ICommand<Reach>
    {
        public void Execute(string[] reqParts, Reach reach, Server<Reach> server)
        {

            lock (server.Clients)
            {
                string registerResponse = string.Empty;
                string availableChatsResponse = string.Empty;
                string chatName = reqParts[0];

                Client<Reach> currClient = new Client<Reach>(chatName, reach);
                server.Clients.Add(currClient);

                IResponseBuilder registerResponseBuilder = new ClientRegisterResponseBuilder(GetAllClientsNamesList(server));
                IResponseBuilder availableChatsResponseBuilder = new GroupChatListResponseBuilder(GetAllGroupChats(server));
                registerResponse = registerResponseBuilder.BuildResponse();
                availableChatsResponse = availableChatsResponseBuilder.BuildResponse();

                for (int i = 0; i < server.Clients.Count; i++)
                {
                    server.ServerSend(registerResponse, server.Clients[i]);
                }
                for (int i = 0; i < server.Chats.Count; i++)
                {
                    server.ServerSend(availableChatsResponse, server.Clients[i]);
                }
            }
        }
        private List<string> GetAllGroupChats(Server<Reach> server)
        {
            List<string> chatsNames = new List<string>();
            for (int i = 0; i < server.Chats.Count; i++)
            {
                chatsNames.Add(server.Chats[i].Info.Name);
            }
            return chatsNames;
        }

        public List<string> GetAllClientsNamesList(Server<Reach> server) 
        {
            
            List<string> clientsNames = new List<string>();
            for (int i = 0; i < server.Clients.Count; i++)
            {
                clientsNames.Add(server.Clients[i].Name);
            }
            return clientsNames;
        }
    }
}
