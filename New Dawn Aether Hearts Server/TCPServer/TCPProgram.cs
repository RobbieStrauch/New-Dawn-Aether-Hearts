using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

public class TCPServerProgram
{
    static Dictionary<int, Socket> clients = new Dictionary<int, Socket>();
    static string client1 = "";
    static string client2 = "";

    public static void Main(String[] args)
    {
        int counter = 1;
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ip = IPAddress.Parse("127.0.0.1");

        try
        {
            server.Bind(new IPEndPoint(ip, 8888));
            server.Listen(2);
            Console.WriteLine(ip.ToString());
            Console.WriteLine("Server Is Running...");

            while (true)
            {
                Socket client = server.Accept();
                clients.Add(counter, client);
                Console.WriteLine(client.RemoteEndPoint.ToString() + "'s Client Connected...");

                Thread thread = new Thread(NewClient);
                thread.Start(counter);
                counter++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }

    public static void NewClient(object o)
    {
        try
        {
            int id = (int)o;
            Socket client;
            client = clients[id];

            while (true)
            {
                byte[] buffer = new byte[512];
                int receive = client.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                if (receive == 0)
                {
                    break;
                }

                string data = Encoding.ASCII.GetString(buffer, 0, receive);
                if (data.Contains("$<PLAYER1$>") && client1 == "")
                {
                    client1 = data;
                    SendData(data + "Approved");
                    Console.WriteLine(data);
                }
                if (data.Contains("$<PLAYER2$>") && client2 == "")
                {
                    client2 = data;
                    SendData(data + "Approved");
                    Console.WriteLine(data);
                }
                if (client1 != string.Empty || client2 != string.Empty)
                {
                    SendData("$<STARTGAME>$");
                    Console.WriteLine(data);
                }
                if (!(data.Contains("$<PLAYER1$>")) || !(data.Contains("$<PLAYER2$>")))
                {
                    SendData(data);
                    Console.WriteLine(data);
                }
            }

            if (client1.Contains("$<PLAYER1$>"))
            {
                client1 = "";
            }
            if (client2.Contains("$<PLAYER2$>"))
            {
                client2 = "";
            }
            clients.Remove(id);
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }

    public static void SendData(string data)
    {
        try
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + "\n");
            foreach (Socket client in clients.Values)
            {
                client.Send(buffer);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("EXCEPTION: " + e.ToString());
        }
    }
}