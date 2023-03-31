using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

public class MainServerProgram
{
    // TCP Stuff
    private static byte[] buffer = new byte[512];
    private static Socket TCPserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static List<Socket> TCPclients = new List<Socket>();
    private static bool startGame = false;
    static string client1 = "";
    static string client2 = "";

    // UDP Stuff
    private static byte[] receiveBuffer = new byte[512];
    static List<EndPoint> UDPclients = new List<EndPoint>();
    static Socket UDPserver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    public static void Main(string[] args)
    {
        IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ip = hostInfo.AddressList[hostInfo.AddressList.Length - 1];
        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8889);

        try
        {
            // TCP Stuff
            TCPserver.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
            TCPserver.Listen(2);
            Console.WriteLine("Starting Server...");
            Thread sendThread = new Thread(new ThreadStart(TCPAcceptClient));
            sendThread.Name = "Send Thread";
            sendThread.Start();


            UDPserver.Bind(localEP);
            Console.WriteLine("Waiting For Data...");
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            UDPserver.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, 0, ref remoteEP, new AsyncCallback(UDPReceiveCallback), remoteEP);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.ReadKey();
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
            Console.WriteLine("Client Connected...");
            TCPclients.Add(socket);

            socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(TCPReceiveCallback), socket);

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
                Array.Copy(buffer, bytes, receive);

                string data = Encoding.ASCII.GetString(bytes);

                if (receive == 0 || data.Contains("$<PLAYER1$>VICTORY") || data.Contains("$<PLAYER2$>VICTORY"))
                {
                    if (client1.Contains("$<PLAYER1$>"))
                    {
                        client1 = "";
                    }
                    if (client2.Contains("$<PLAYER2$>"))
                    {
                        client2 = "";
                    }
                    startGame = false;
                    TCPclients.Remove(socket);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
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

                    socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(TCPReceiveCallback), socket);
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
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            int receive = UDPserver.EndReceiveFrom(result, ref remoteEP);
            byte[] data = new byte[receive];
            Array.Copy(receiveBuffer, data, receive);
            string message = Encoding.ASCII.GetString(data);

            if (!UDPclients.Contains(remoteEP))
            {
                Console.WriteLine("Added Client: " + remoteEP.ToString());
                UDPclients.Add(remoteEP);
            }

            Console.WriteLine("Receive From: " + remoteEP.ToString() + " | Data: " + message);

            UDPSendToAll(message);

            EndPoint newClientEP = new IPEndPoint(IPAddress.Any, 0);

            UDPserver.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, 0, ref newClientEP, new AsyncCallback(UDPReceiveCallback), newClientEP);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void UDPSendCallback(IAsyncResult result)
    {
        UDPserver.EndSendTo(result);
    }

    public static void UDPSendTo(byte[] data, EndPoint clientEP)
    {
        try
        {
            //server.SendTo(data, clientEP);
            UDPserver.BeginSendTo(data, 0, data.Length, 0, clientEP, new AsyncCallback(UDPSendCallback), clientEP);
        }
        catch (Exception)
        {
            UDPclients.Remove(clientEP);
        }
    }

    public static void UDPSendToAll(string data)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(data);

        foreach (var client in UDPclients)
        {
            UDPSendTo(buffer, client);
        }
    }
}
