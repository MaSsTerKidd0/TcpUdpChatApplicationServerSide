namespace Server.ChatObserver
{
    public class ChatInfo
    {
        #region Fields
        private List<string> _messages = new List<string>();
        #endregion

        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public List<string> Messages { get { return _messages; } }
        #endregion

        public void AddMessage(string msg)
        {
            _messages.Add(msg);
        }
    }
}
