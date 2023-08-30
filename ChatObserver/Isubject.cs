using Server.Client;

namespace Server.ChatObserver
{
    public interface ISubject<T>
    {
        void Attach(Client<T> observer);
        void Detach(Client<T> observer);
        void Notify(string message);
    }
}
