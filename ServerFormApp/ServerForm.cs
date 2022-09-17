using SocketHelper;
using System.Net;
using System.Net.Sockets;

namespace ServerFormApp;

public partial class ServerForm : Form
{
    private SocketServer _socketServer;
    public ServerForm()
    {
        InitializeComponent();
        _socketServer = new SocketServer();
        _socketServer.RaiseClientConnectedEvent += HandleClientConnected;
        _socketServer.RaiseMessageClientEvent += HandleMessageRecived;
    }

    public void AcceptIncomingOnSocket()
    {
        var listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        var ipAddr = IPAddress.Any;
        var ipEp = new IPEndPoint(ipAddr, 2300);

        listenerSocket.Bind(ipEp);
        listenerSocket.Listen(5);
        Console.WriteLine("About to accept incoming connection.");
        var client = listenerSocket.Accept();
        Console.WriteLine("Client connected. " + client.ToString() + " - IP End Point: " + client?.RemoteEndPoint?.ToString());

    }

    private async void BtnListenConnection(object sender, EventArgs e)
    {
        _socketServer.StartListeningIncomingConnection();
    }

    private void BtnSendMessageToAllClients(object sender, EventArgs e)
    {
        _socketServer.SendToAll(txtBoxMessage.Text);
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void BtnStopServer(object sender, EventArgs e)
    {
        _socketServer.StopServer();
    }

    private void BehaviorFormmClosing(object sender, FormClosingEventArgs e)
    {
        _socketServer.StopServer();
    }

    void HandleClientConnected(object sender, ConnectServerEvent clientConnectedEvent)
    {
        txtShowMessages.AppendText($"{DateTime.Now} - New Client Connected: {clientConnectedEvent.Client} \r\n");
    }
    void HandleMessageRecived(object sender, MessageClientEvent messageClientEvent)
    {
        txtShowMessages.AppendText($"{DateTime.Now} - Recived From  {messageClientEvent.ClientIp}  Message : {messageClientEvent.Message} \r\n");
    }
}