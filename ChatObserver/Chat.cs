using Server.Client;

namespace Server.ChatObserver
{
    public class Chat<Reach> : ISubject<Reach>
    {
        #region Fields
        private ChatInfo _info;
        private List<Client<Reach>> _members;
        public ChatInfo Info { get { return _info; } }
        static int _id = 0;
        private Server<Reach> _server;
        #endregion

        #region init
        public Chat(string chatName, Server<Reach> server)
        {
            _info = new ChatInfo();
            _info.ID = _id++;
            _members = new List<Client<Reach>>();
            Info.Name = chatName;
            _server = server;
        }
        #endregion

        #region ObserverActions
        public void Attach(Client<Reach> observer)
        {
            _members.Add(observer);
        }

        public void Detach(Client<Reach> observer)
        {
            if(_members.Contains(observer))
                _members.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (Client<Reach> member in _members)
            {
                _server.ServerSend(message, member);
            }

        }
        #endregion
    }
}
