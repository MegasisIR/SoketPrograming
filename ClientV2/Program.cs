
using SocketHelper;
using SocketHelper.Events;

var client = new SokectClient();
client.RiseMessageServerEvent += HandleMessageRecived;
Console.WriteLine("Please Type a Valid Server IP Address And Press Enter : ");
var serverIp = Console.ReadLine();

Console.WriteLine("Please Supply a Valid Port Number 0 - 65535 and Press Enter : ");
var portNumber = Console.ReadLine();

if (serverIp.StartsWith("<HOST>"))
{
    serverIp = serverIp.Replace("<HOST>",String.Empty);
    serverIp = client.ResolveHostNameIpAddress(serverIp);
}

if (!client.SetServerIpAddress(serverIp) || !client.SetPortNumber(portNumber))
{
    Console.WriteLine($"Wrong ip Address or port number supplied {serverIp} - {portNumber}");
    return;
}
string strInputUser = null;
client.ConnectToServer();

do
{
    strInputUser = Console.ReadLine();
    if (!strInputUser.Equals("x"))
    {
        client.SendToServer(strInputUser);
    }else if (strInputUser.Equals("x"))
    {
        client.CloseAndDisconnect();
    }
} while (strInputUser != "x");

 void HandleMessageRecived(object sender,MessageServerEvent messageServerEvent)
{
    Console.WriteLine($"{DateTime.Now} recived from {messageServerEvent.IP} message : {messageServerEvent.Message}");
}