using Server.ChatObserver;
using Server.Client;
using Server.Protocols;
using Server.RequestsCommands;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public abstract class Server<Reach> : ServerInterface
    {
        #region Fields
        protected Socket _listener;
        protected int _port;
        protected IPAddress _ipAddress;
        protected IPEndPoint _endPoint;

        private Dictionary<string, ICommand<Reach>> requestsCommandDictionary;
        private const string EXIT = "300";

        private List<Client<Reach>> _clients = new List<Client<Reach>>();
        private List<Chat<Reach>> _chats = new List<Chat<Reach>>();
        #endregion

        #region Properties
        public List<Chat<Reach>>Chats { get { return _chats; } }
        public List<Client<Reach>> Clients { get { return _clients; } }
        #endregion

        #region InitServer
        protected Server()
        {
            _ipAddress = IPAddress.Loopback;
            _endPoint = new IPEndPoint(_ipAddress, _port);
            requestsCommandDictionary = new Dictionary<string, ICommand<Reach>>();
            InitializeRequestCommandDictionary();
        }

        public void ExecuteServer()
        {
            try
            {
                InitConnection();
                ServerStart();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                TerminateSocket();
            }
        }

        private void InitializeRequestCommandDictionary()
        {
            requestsCommandDictionary.Add("100", new ClientRegisterCommand<Reach>());
            requestsCommandDictionary.Add("102", new SendPrivateMessageCommand<Reach>());
            requestsCommandDictionary.Add("200", new CreateChatCommand<Reach>());
            requestsCommandDictionary.Add("201", new JoinChatCommand<Reach>());
            requestsCommandDictionary.Add(EXIT, new CloseClientConnectionCommand<Reach>());
        }
        #endregion

        #region RequestHandeling
        protected virtual void HandleRequest(string request, Reach reach)
        {
            string[] reqParts = request.Split('#');
            string reqTyp = reqParts[0];
            if (request.Length > 0 && requestsCommandDictionary.ContainsKey(reqTyp))
            {
                requestsCommandDictionary[reqTyp].Execute(reqParts.Skip(1).ToArray(), reach, this);
            }
            else
            {
                Console.WriteLine("Invalid request code.");
            }
        }
        #endregion

        private void TerminateSocket()
        {
            foreach (Client<Reach> client in _clients)
            {
                requestsCommandDictionary[EXIT].Execute(null, client.ClientReach, this);
            }
            _listener.Shutdown(SocketShutdown.Both);
            _listener.Close();
            _listener.Dispose();
        }
        
        #region Abstracts
        protected abstract void ServerStart();
        protected abstract void InitConnection();
        protected abstract void Send(string msg, Client<Reach> client);
        #endregion

        public void ServerSend(string msg, Client<Reach> client) 
        {
            Send(msg, client);
        }
    }
}