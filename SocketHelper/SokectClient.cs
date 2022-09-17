using SocketHelper.Events;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;

namespace SocketHelper;

public class SokectClient
{
    private IPAddress _ipAddressServer;
    private int _portServer;
    private TcpClient? _server;
    public EventHandler<MessageServerEvent> RiseMessageServerEvent;
    public SokectClient()
    {
        _ipAddressServer = null;
        _portServer = -1;
        _server = null;
    }

    public virtual void OnRaiseMessageServerEvent(MessageServerEvent e)
    {
        var hanlder = RiseMessageServerEvent;
        if (true)
        {
           hanlder(this, e);
        }
    }
    public IPAddress ServerIpAddress => _ipAddressServer;

    public int? ServerPort => _portServer;

    public bool SetServerIpAddress(string _IpAddressServer)
    {
        if (!IPAddress.TryParse(_IpAddressServer, out var ipAddress))
        {
            Console.WriteLine("Invalid server IP supplied.");
            return false;
        }
        _ipAddressServer = ipAddress;
        return true;
    }

    public bool SetPortNumber(string portNumber)
    {
        if (!int.TryParse(portNumber, out var port))
        {
            Console.WriteLine("Invalid port number supplied, return.");
            return false;
        }
        if (port <= 0 || port > 65535)
        {
            Console.WriteLine("Port number must be between 0 and 65535");
            return false;
        }
        _portServer = port;
        return true;
    }

    public async Task ConnectToServer()
    {
        if (_server is null)
        {
            _server = new TcpClient();
        }

        try
        {
            await _server.ConnectAsync(_ipAddressServer, _portServer);
            Console.WriteLine($"Connect to server IP/Port: {_ipAddressServer} / {_portServer}");

            ReadDataAsync(_server);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async Task ReadDataAsync(TcpClient server)
    {
        try
        {
            var clientStreamReader = new StreamReader(_server.GetStream());
            var buffer = new char[64];
            var readByteCount = 0;
            while (true)
            {
                readByteCount= await clientStreamReader.ReadAsync(buffer, 0, buffer.Length);
                if (readByteCount <= 0)
                {
                    Console.WriteLine("Discconnected From Server");
                    server.Close();
                    break;
                }
                var messageRecived = new string(buffer);
                Console.WriteLine($"Received Bytes: {readByteCount} - Message: {messageRecived}");
                var messageServerEvent = new MessageServerEvent(messageRecived, server.Client.RemoteEndPoint.ToString());
                OnRaiseMessageServerEvent(messageServerEvent);
                Array.Clear(buffer, 0, buffer.Length);
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
        }
    }

    public async Task SendToServer(string strInputUser)
    {
        if (string.IsNullOrEmpty(strInputUser))
        {
            Console.WriteLine("Empty string supplied to send. ");
            return;
        }

        if (_server is not null)
        {
            if (_server.Connected)
            {
                var clientStreamWriter = new StreamWriter(_server.GetStream());
                clientStreamWriter.AutoFlush = true;

                await clientStreamWriter.WriteAsync(strInputUser);
                Console.WriteLine("Data is sent ... ");

            }
        }
    }

    public void CloseAndDisconnect()
    {
        if (_server is not null && _server.Connected)
        {
            _server.Close();
            _server.Dispose();
        }
    }

    public string ResolveHostNameIpAddress(string hostName)
    {
        try
        {
           var ipsHost = Dns.GetHostAddresses(hostName);
            return ipsHost.Where(x => x.AddressFamily is AddressFamily.InterNetwork).FirstOrDefault().ToString();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        return String.Empty;
    }
}
