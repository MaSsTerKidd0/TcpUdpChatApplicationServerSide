using Server.ChatObserver;
using Server.Client;
using Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.RequestsCommands
{
    public class CreateChatCommand<Reach> : ICommand<Reach>
    {
        public void Execute(string[] reqParts, Reach reach, Server<Reach> server)
        {
            Chat<Reach> newChat = new Chat<Reach>(reqParts[0], server);
            List<string> chatNames = new List<string>();
            IResponseBuilder chatCreatedBuilder;
            string groupChatsResponse;

            if (newChat.Info.Name != "")
                server.Chats.Add(newChat);

            foreach (Chat<Reach> chat in server.Chats)
            {
                chatNames.Add(chat.Info.Name);
            }

            chatCreatedBuilder = new GroupChatListResponseBuilder(chatNames);
            groupChatsResponse = chatCreatedBuilder.BuildResponse();

            foreach (Client<Reach> client in server.Clients)
            {
                server.ServerSend(groupChatsResponse, client);
            }
        }
    }
}
