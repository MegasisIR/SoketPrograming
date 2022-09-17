public class MessageClientEvent : EventArgs
{
    public string Message { get; private set; }
    public string ClientIp { get; private set; }
    public MessageClientEvent(string message, string clientIp)
    {
        Message=message;
        ClientIp=clientIp;
    }
}
