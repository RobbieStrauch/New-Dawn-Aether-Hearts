using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

public class TCPServerProgram
{
    private static byte[] buffer = new byte[512];
    private static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static List<Socket> clients = new List<Socket>();
    private static bool startGame = false;

    static string client1 = "";
    static string client2 = "";

    static void Main(string[] args)
    {
        try
        {
            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
            server.Listen(2);

            Console.WriteLine("Starting Server...");

            //server.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Thread sendThread = new Thread(new ThreadStart(AcceptClient));
            sendThread.Name = "Send Thread";
            sendThread.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.ReadKey();
    }

    private static void AcceptClient()
    {
        try
        {
            server.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    private static void AcceptCallback(IAsyncResult result)
    {
        try
        {
            Socket socket = server.EndAccept(result);
            Console.WriteLine("Client Connected...");
            clients.Add(socket);

            socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);

            server.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
        }
    }

    private static void ReceiveCallback(IAsyncResult result)
    {
        if (clients.Count > 0)
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
                    clients.Remove(socket);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                else
                {
                    if (data.Contains("$<PLAYER1$>") && client1 == "")
                    {
                        client1 = data;
                        data += "Approved";
                    }
                    if (data.Contains("$<PLAYER2$>") && client2 == "")
                    {
                        client2 = data;
                        data += "Approved";
                    }
                    if ((client1 != string.Empty || client2 != string.Empty) && !startGame)
                    {
                        data += "$<STARTGAME>$";
                        startGame = true;
                    }

                    SendData(data);

                    socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }
    }

    //private static void ReceiveCallback(IAsyncResult result)
    //{
    //    Socket socket = (Socket)result.AsyncState;
    //    int receive = socket.EndReceive(result);
    //    byte[] bytes = new byte[receive];
    //    Array.Copy(buffer, bytes, receive);

    //    string data = Encoding.ASCII.GetString(bytes);

    //    //string data = Encoding.ASCII.GetString(buffer, 0, receive);
    //    if (data.Contains("$<PLAYER1$>") && client1 == "")
    //    {
    //        client1 = data;
    //        SendData(data + "Approved");
    //        Console.WriteLine(data);
    //    }
    //    if (data.Contains("$<PLAYER2$>") && client2 == "")
    //    {
    //        client2 = data;
    //        SendData(data + "Approved");
    //        Console.WriteLine(data);
    //    }
    //    if (client1 != string.Empty || client2 != string.Empty)
    //    {
    //        SendData("$<STARTGAME>$");
    //        Console.WriteLine(data);
    //    }
    //    if (!(data.Contains("$<PLAYER1$>")) || !(data.Contains("$<PLAYER2$>")))
    //    {
    //        SendData(data);
    //        Console.WriteLine(data);
    //    }

    //    socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);
    //}

    private static void SendCallback(IAsyncResult result)
    {
        Socket socket = (Socket)result.AsyncState;
        socket.EndSend(result);
    }

    public static void SendData(string data)
    {
        try
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + "\n");
            Console.WriteLine(data);
            foreach (Socket client in clients)
            {
                //client.Send(buffer);
                client.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(SendCallback), client);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }

    //private static void SendLoop()
    //{
    //    while (true)
    //    {
    //        sendBuffer = Encoding.ASCII.GetBytes(sendMessage);

    //        foreach (var client in clients)
    //        {
    //            Console.WriteLine("Sent To: " + client.RemoteEndPoint.ToString());
    //            client.BeginSend(sendBuffer, 0, sendBuffer.Length, 0, new AsyncCallback(SendCallback), client);
    //        }

    //        sendMessage = "";
    //        Thread.Sleep(1000);
    //    }
    //}
}
