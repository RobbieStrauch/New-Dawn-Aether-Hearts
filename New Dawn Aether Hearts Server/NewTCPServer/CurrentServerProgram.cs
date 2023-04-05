using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

public class Highscore
{
    public string name;
    public int score;
}

public class MainServerProgram
{
    // DLL Stuff
    public static List<Highscore> highscores = new List<Highscore>();
    public static List<string> readLines = new List<string>();
    private static byte[] DLLbuffer = new byte[512];
    private static Socket DLLserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static List<Socket> DLLclients = new List<Socket>();
    static string path = Path.GetFullPath(Path.GetFileName("ClientHighscores.txt"));

    // TCP Stuff
    private static byte[] TCPbuffer = new byte[512];
    private static Socket TCPserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static List<Socket> TCPclients = new List<Socket>();
    private static bool startGame = false;
    static string client1 = "";
    static string client2 = "";

    // UDP Stuff
    static List<IPEndPoint> UDPclients = new List<IPEndPoint>();

    public static void Main(string[] args)
    {
        Read(path);

        for (int i = 0; i < readLines.Count; i++)
        {
            string[] temp_string = readLines[i].Split('|');
            Highscore temp_highscore = new Highscore();
            temp_highscore.name = temp_string[0];
            temp_highscore.score = int.Parse(temp_string[1]);
            highscores.Add(temp_highscore);
        }

        try
        {
            // DLL Stuff
            DLLserver.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8887));
            DLLserver.Listen(10);
            Console.WriteLine("Starting Highscore Server...");
            Thread highscoreThread = new Thread(new ThreadStart(DLLAcceptClient));
            highscoreThread.Name = "Highscore Thread";
            highscoreThread.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ip = hostInfo.AddressList[hostInfo.AddressList.Length - 1];
        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8889);

        try
        {
            // TCP Stuff
            TCPserver.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
            TCPserver.Listen(2);
            Console.WriteLine("Starting Chat Server...");
            Thread sendThread = new Thread(new ThreadStart(TCPAcceptClient));
            sendThread.Name = "Send Thread";
            sendThread.Start();

            // UDP Stuff
            UdpClient udpSocket = new UdpClient(localEP);
            const int SIO_UDP_CONNRESET = -1744830452;
            udpSocket.Client.IOControl((IOControlCode) SIO_UDP_CONNRESET, new byte[] { 0, 0, 0, 0 }, null);
            udpSocket.BeginReceive(new AsyncCallback(UDPReceiveCallback), udpSocket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.ReadKey();
    }

    private static void DLLAcceptClient()
    {
        try
        {
            DLLserver.BeginAccept(new AsyncCallback(DLLAcceptCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    private static void DLLAcceptCallback(IAsyncResult result)
    {
        try
        {
            Socket socket = DLLserver.EndAccept(result);
            Console.WriteLine("Highscore Client Connected...");
            DLLclients.Add(socket);

            socket.BeginReceive(DLLbuffer, 0, DLLbuffer.Length, 0, new AsyncCallback(DLLReceiveCallback), socket);

            DLLserver.BeginAccept(new AsyncCallback(DLLAcceptCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    private static void DLLReceiveCallback(IAsyncResult result)
    {
        if (DLLclients.Count > 0)
        {
            try
            {
                Socket socket = (Socket)result.AsyncState;
                int receive = socket.EndReceive(result);
                byte[] bytes = new byte[receive];
                Array.Copy(DLLbuffer, bytes, receive);

                string data = Encoding.ASCII.GetString(bytes);

                if (receive == 0)
                {
                    Console.WriteLine("Disconnect");
                    string temp = "";
                    for (int i = 0; i < highscores.Count; i++)
                    {
                        temp += highscores[i].name + "|" + highscores[i].score + "\n";
                    }

                    Write(path, temp);

                    DLLclients.Remove(socket);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                else
                {
                    if (data.Contains("{<HIGHSCORE>}"))
                    {
                        bool check = false;
                        string[] temp_string = data.Split('|');
                        Highscore temp_highscore = new Highscore();
                        temp_highscore.name = temp_string[1];
                        temp_highscore.score = int.Parse(temp_string[2]);

                        foreach (var item in highscores)
                        {
                            if (item.name == temp_highscore.name)
                            {
                                item.score = temp_highscore.score;
                                check = true;
                                break;
                            }
                        }
                        if (!check)
                        {
                            highscores.Add(temp_highscore);
                        }
                    }

                    string temp = "";
                    foreach (var item in highscores)
                    {
                        temp += "|" + item.name + "|" + item.score;
                    }

                    DLLSendAllData("{<NEWHIGHSCORE>}|" + highscores.Count + temp);
                    Console.WriteLine(socket.RemoteEndPoint.ToString() + ": " + data);

                    socket.BeginReceive(DLLbuffer, 0, DLLbuffer.Length, 0, new AsyncCallback(DLLReceiveCallback), socket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }
    }

    private static void DLLSendCallback(IAsyncResult result)
    {
        Socket socket = (Socket)result.AsyncState;
        socket.EndSend(result);
    }

    public static void DLLSendData(Socket socket, string data)
    {
        try
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            socket.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(DLLSendCallback), socket);
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }

    public static void DLLSendAllData(string data)
    {
        try
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            foreach (Socket client in DLLclients)
            {
                client.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(DLLSendCallback), client);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }

    private static void TCPAcceptClient()
    {
        try
        {
            TCPserver.BeginAccept(new AsyncCallback(TCPAcceptCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    private static void TCPAcceptCallback(IAsyncResult result)
    {
        try
        {
            Socket socket = TCPserver.EndAccept(result);
            Console.WriteLine("Chat Client Connected...");
            TCPclients.Add(socket);

            socket.BeginReceive(TCPbuffer, 0, TCPbuffer.Length, 0, new AsyncCallback(TCPReceiveCallback), socket);

            TCPserver.BeginAccept(new AsyncCallback(TCPAcceptCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    private static void TCPReceiveCallback(IAsyncResult result)
    {
        if (TCPclients.Count > 0)
        {
            try
            {
                Socket socket = (Socket)result.AsyncState;
                int receive = socket.EndReceive(result);
                byte[] bytes = new byte[receive];
                Array.Copy(TCPbuffer, bytes, receive);

                string data = Encoding.ASCII.GetString(bytes);

                if (receive == 0 || data.Contains("$<PLAYER1$>VICTORY") || data.Contains("$<PLAYER2$>VICTORY") || data.Contains("$<PLAYER1$>DEFEAT") || data.Contains("$<PLAYER2$>DEFEAT"))
                {
                    if (data.Contains("$<PLAYER1$>DEFEAT") || data.Contains("$<PLAYER2$>DEFEAT"))
                    {
                        if (client1.Contains(socket.RemoteEndPoint.ToString()))
                        {
                            client1 = "";
                            Console.WriteLine("Removed Client1: " + socket.RemoteEndPoint.ToString() + " | " + data);
                        }
                        if (client2.Contains(socket.RemoteEndPoint.ToString()))
                        {
                            client2 = "";
                            Console.WriteLine("Removed Client2: " + socket.RemoteEndPoint.ToString() + " | " + data);
                        }

                        startGame = false;
                        TCPclients.Remove(socket);
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();

                        TCPSendAllData(data);
                    }
                    if (data.Contains("$<PLAYER1$>VICTORY") || data.Contains("$<PLAYER2$>VICTORY"))
                    {
                        if (client1.Contains(socket.RemoteEndPoint.ToString()))
                        {
                            client1 = "";
                            Console.WriteLine("Removed Client1: " + socket.RemoteEndPoint.ToString() + " | " + data);
                        }
                        if (client2.Contains(socket.RemoteEndPoint.ToString()))
                        {
                            client2 = "";
                            Console.WriteLine("Removed Client2: " + socket.RemoteEndPoint.ToString() + " | " + data);
                        }

                        startGame = false;
                        TCPclients.Remove(socket);
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                }
                else
                {
                    if (data.Contains("$<PLAYER1$>") && client1 == "")
                    {
                        client1 = socket.RemoteEndPoint.ToString();
                        data += "Approved";
                    }
                    if (data.Contains("$<PLAYER2$>") && client2 == "")
                    {
                        client2 = socket.RemoteEndPoint.ToString();
                        data += "Approved";
                    }
                    if ((client1 != string.Empty || client2 != string.Empty) && !startGame)
                    {
                        data += "$<STARTGAME>$";
                        startGame = true;
                    }

                    TCPSendAllData(data);
                    Console.WriteLine(socket.RemoteEndPoint.ToString() + ": " + data);

                    socket.BeginReceive(TCPbuffer, 0, TCPbuffer.Length, 0, new AsyncCallback(TCPReceiveCallback), socket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }
    }

    private static void TCPSendCallback(IAsyncResult result)
    {
        Socket socket = (Socket)result.AsyncState;
        socket.EndSend(result);
    }

    public static void TCPSendData(Socket socket, string data)
    {
        try
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            socket.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(TCPSendCallback), socket);
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }

    public static void TCPSendAllData(string data)
    {
        try
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            foreach (Socket client in TCPclients)
            {
                client.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(TCPSendCallback), client);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }

    public static void UDPReceiveCallback(IAsyncResult result)
    {
        try
        {
            UdpClient socket = result.AsyncState as UdpClient;
            IPEndPoint source = new IPEndPoint(0, 0);
            byte[] data = socket.EndReceive(result, ref source);
            string message = Encoding.ASCII.GetString(data);

            if (!UDPclients.Contains(source))
            {
                Console.WriteLine("Added Client: " + source.ToString());
                UDPclients.Add(source);
            }

            Console.WriteLine("Receive From: " + source.ToString() + " | Data: " + message);

            UDPSendToAll(socket, message);

            socket.BeginReceive(new AsyncCallback(UDPReceiveCallback), socket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void UDPSendTo(UdpClient client, byte[] data, IPEndPoint clientEP)
    {
        try
        {
            client.Send(data, data.Length, clientEP);
        }
        catch (Exception)
        {
            UDPclients.Remove(clientEP);
        }
    }

    public static void UDPSendToAll(UdpClient client, string data)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(data);

        foreach (var item in UDPclients)
        {
            UDPSendTo(client, buffer, item);
        }
    }

    public static void Read(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            if (line.Length > 1)
            {
                readLines.Add(line);
            }
        }

        streamReader.Close();
    }

    public static void Write(string path, string data) 
    {
        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(data);
        streamWriter.Close();
    }
}
