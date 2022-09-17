using System.Net;
using System.Net.Sockets;
using System.Text;

var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    Console.WriteLine("Please Type a Valid Server IP Address and Press Enter: ");

    var strIpAddress = Console.ReadLine();

    Console.WriteLine("Please Supply a Valid Port Number 0 - 65535 and Press Enter: ");
    var strPortInput = Console.ReadLine();

    if (strIpAddress == " ") strIpAddress = "127.0.0.1";
    if (strPortInput == " ") strPortInput = "2300";

    if (!IPAddress.TryParse(strIpAddress, out var ipAddress))
    {
        Console.WriteLine("Invalid server IP supplied.");
        return;
    }

    if (!int.TryParse(strPortInput?.Trim(), out var nPortInput))
    {
        Console.WriteLine("Invalid port number supplied, return.");
        return;
    }

    if (nPortInput <= 0 || nPortInput > 65535)
    {
        Console.WriteLine("Port number must be between 0 and 65535.");
        return;
    }

    Console.WriteLine($"IPAddress: {ipAddress.ToString()} - Port: {nPortInput}");

    server.Connect(ipAddress, nPortInput);

    Console.WriteLine(
        "Connected to the server, type text and press enter to send it to the server, type <EXIT> to close.");


    while (true)
    {
        var inputCommand = Console.ReadLine();

        if (inputCommand is not null && inputCommand.Equals("<EXIT>")) break;

        if (inputCommand != null)
        {
            byte[] buffSend = Encoding.ASCII.GetBytes(inputCommand);

            server.Send(buffSend);
        }

        byte[] buffReceived = new byte[128];
        int countBits = server.Receive(buffReceived);

        Console.WriteLine("Data received: {0}", Encoding.ASCII.GetString(buffReceived, 0, countBits));
    }
}
catch (Exception exception)
{
    Console.WriteLine(exception.ToString());
}
finally
{
    if (server != null)
    {
        if (server.Connected)
        {
            server.Shutdown(SocketShutdown.Both);
        }

        server.Close();
        server.Dispose();
    }
}

Console.WriteLine("Press a key to exit...");
Console.ReadKey();