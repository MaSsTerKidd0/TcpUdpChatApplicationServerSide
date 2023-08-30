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
    public class SendPrivateMessageCommand<Reach> : ICommand<Reach>
    {
        private enum RequestPartIndex
        {
            From = 0,
            To = 1,
            ArrivalTime = 2,
            MsgToSend = 3
        }
        public void Execute(string[] reqParts, Reach reach, Server<Reach> server)
        {
            string from = reqParts[(int)RequestPartIndex.From];
            string to = reqParts[(int)RequestPartIndex.To];
            string arrivalTime = reqParts[(int)RequestPartIndex.ArrivalTime];
            string msgToSend = reqParts[(int)RequestPartIndex.MsgToSend];

            IResponseBuilder messageResponseBuilder = new MessageSentResponseBuilder(from, to, arrivalTime , msgToSend);
            string response = messageResponseBuilder.BuildResponse();
            Chat<Reach> chat;

            chat = server.Chats.Find(c => c.Info.Name == to);
            if (chat != null)
            {
                chat.Notify(response);
            }
            else
            {
                Client<Reach> clientToSend = server.Clients.Find(c => c.Name == to);
                Client<Reach> client = server.Clients.Find(c => c.Name == from);
                server.ServerSend(response, clientToSend);
                server.ServerSend(response, client);
            }
        }
    }
}
