using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SocketHelper;

public class SocketServer
{
    private IPAddress? _ipAddress;
    private int _port;
    private TcpListener? _tcpListener;
    public bool KeepRunning { get;private set; }

    private List<TcpClient> _clients;
    public EventHandler<ConnectServerEvent> RaiseClientConnectedEvent;
    public EventHandler<MessageClientEvent> RaiseMessageClientEvent;


    protected virtual void OnRaiseClientConnectedEvent(ConnectServerEvent e)
    {
        var handler = RaiseClientConnectedEvent;
        if(handler != null)
        {
            handler(this, e);
        }
    } 
    protected virtual void OnRaiseMessageClientEvent(MessageClientEvent e)
    {
        var handler = RaiseMessageClientEvent;
        if(handler != null)
        {
            handler(this, e);
        }
    }

    public SocketServer()
    {
        _clients= new List<TcpClient>();
    }

    public async void StartListeningIncomingConnection(int port = 2300, IPAddress? ipAddress = null)
    {
        ipAddress ??= IPAddress.Any;
        if (port <= 0) port = 2300;

        _port = port;
        _ipAddress = ipAddress;
        Debug.WriteLine($"IP Address: {_ipAddress} - Port : {_port}");
        _tcpListener = new TcpListener(_ipAddress, _port);

        try
        {
            _tcpListener.Start();
            KeepRunning = true;
            while (KeepRunning)
            { 
                var returnedByAccept = await _tcpListener.AcceptTcpClientAsync();
                _clients.Add(returnedByAccept);
                Debug.WriteLine($"Client Connected successfully {returnedByAccept.Client.RemoteEndPoint}, Count : {_clients.Count()}");
                TakeCareTCPClient(returnedByAccept);

                var clientConnected = new ConnectServerEvent(returnedByAccept.Client.RemoteEndPoint.ToString());
                OnRaiseClientConnectedEvent(clientConnected);
            }
        }
        catch (Exception exp)
        {
            Debug.WriteLine($"Client Connected successfully: {exp.Message}");
        }
    }

    private async void TakeCareTCPClient(TcpClient paramClient)
    {
        NetworkStream? stream = null;
        StreamReader? reader = null;

        try
        {
            stream = paramClient.GetStream();
            reader = new StreamReader(stream);
            char[] buffer = new char[64];
            while (KeepRunning)
            {
                Debug.WriteLine("*** Ready to read ***");
                int bytes = await reader.ReadAsync(buffer, 0, buffer.Length);
                Debug.WriteLine("Returned : "+bytes);
                if (bytes == 0)
                {
                    RemoveClient(paramClient);
                    Debug.WriteLine("Socket disconnected");
                    break;
                }

                var recivedText = new String(buffer);
                Debug.WriteLine($"*** Recived data from client {paramClient.Client.RemoteEndPoint} equal with : "+recivedText);
                var reseiveMessageEvent = new MessageClientEvent(recivedText, paramClient.Client.RemoteEndPoint.ToString());
                OnRaiseMessageClientEvent(reseiveMessageEvent);
                Array.Clear(buffer, 0, bytes);
            }
        }
        catch (Exception exp)
        {
            RemoveClient(paramClient);
            Debug.WriteLine(exp.Message);
        }
    }

    private void RemoveClient(TcpClient paramClient)
    {
        if (_clients.Contains(paramClient))
        {
            _clients.Remove(paramClient);
            Debug.WriteLine($"Client {paramClient.Client.RemoteEndPoint} removed, count: {_clients.Count}");
        }
    }

    public async void SendToAll(string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        try
        {
            byte[] bufferMessage = Encoding.UTF8.GetBytes(message+"\r\n");
            _clients.ForEach(async client =>
            {
                await client.GetStream().WriteAsync(bufferMessage, 0, bufferMessage.Length);
            });
        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex.Message);
        }
    }

    public void StopServer()
    {
        try
        {
            if (_tcpListener is not null)
                _tcpListener.Stop();
            _clients.ForEach(client =>
            {
                client.Close();
                client.Dispose();
            });
            _clients.Clear();
        }
        catch (Exception exp)
        {
            Debug.WriteLine(exp.Message);
        }
    }
}