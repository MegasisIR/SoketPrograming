using System.Net;
using System.Net.Sockets;
using System.Text;

var listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var ipAddr = IPAddress.Any;
var ipEp = new IPEndPoint(ipAddr, 2300);

try
{
    listenerSocket.Bind(ipEp);
    listenerSocket.Listen(5);
    Console.WriteLine("About to accept incoming connection.");
    var client = listenerSocket.Accept();

    Console.WriteLine("About to accept incoming connection.");

    Console.WriteLine("Client connected. " + client.ToString() + " - IP End Point: " + client?.RemoteEndPoint?.ToString());

    var buffer = new byte[256];

    int numberOfReceivedBytes = 0;
    while (true)
    {
        if (client != null) numberOfReceivedBytes = client.Receive(buffer);

        Console.WriteLine("Number of received bytes: " + numberOfReceivedBytes);
        Console.WriteLine("Data sent by client is: " + buffer);

        var receivedText = Encoding.UTF8.GetString(buffer,0,numberOfReceivedBytes);
        Console.WriteLine("Data sent by client is : " + receivedText);

        if (receivedText.Equals("x")) break;
        Array.Clear(buffer,0,buffer.Length);
        numberOfReceivedBytes = 0;
    }
}
catch(Exception exception)
{
    Console.WriteLine(exception.Message);
}