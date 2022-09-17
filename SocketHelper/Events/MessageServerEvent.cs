

namespace SocketHelper.Events;

public class MessageServerEvent : EventArgs
{
    public string Message { get; private set; }
    public string IP { get;private set; }
    public MessageServerEvent(string message, string clientIP)
    {
        Message=message;
        IP=clientIP;
    }
}
