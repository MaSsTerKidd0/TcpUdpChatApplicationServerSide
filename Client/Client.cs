namespace Server.Client
{
    public class Client<Reach>
    {
        private static int _id;
        private string _name;
        private Reach _reach;


        public int ID { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Reach ClientReach 
        {
            get { return _reach; }
            set { _reach = value; }
        }

        public Client(string name, Reach reach) 
        {
            ID = _id++;
            Name = name;
            ClientReach = reach;
        }
    }
}
