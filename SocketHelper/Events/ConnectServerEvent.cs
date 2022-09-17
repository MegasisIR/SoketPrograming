public class ConnectServerEvent : EventArgs
{
    public string Client { get; private set; }

    public ConnectServerEvent(string client)
    {
        Client=client;
    }
}
