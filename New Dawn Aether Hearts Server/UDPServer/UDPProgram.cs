using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class UDPServerProgram
{
    private static byte[] receiveBuffer = new byte[512];
    static List<EndPoint> clientList = new List<EndPoint>();
    static Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    public static int Main(String[] args)
    {
        IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ip = hostInfo.AddressList[hostInfo.AddressList.Length - 1];
        IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8889);

        try
        {
            server.Bind(localEP);
            Console.WriteLine("Waiting For Data...");

            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            server.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, 0, ref remoteEP, new AsyncCallback(ReceiveCallback), remoteEP);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Exception: " + exception.ToString());
        }
        Console.ReadKey();
        return 0;
    }

    public static void ReceiveCallback(IAsyncResult result)
    {
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            int receive = server.EndReceiveFrom(result, ref remoteEP);
            byte[] data = new byte[receive];
            Array.Copy(receiveBuffer, data, receive);
            string message = Encoding.ASCII.GetString(data);

            if (!clientList.Contains(remoteEP))
            {
                Console.WriteLine("Added Client: " + remoteEP.ToString());
                clientList.Add(remoteEP);
            }

            Console.WriteLine("Receive From: " + remoteEP.ToString() + " | Data: " + message);

            SendToAll(message);

            EndPoint newClientEP = new IPEndPoint(IPAddress.Any, 0);

            server.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, 0, ref newClientEP, new AsyncCallback(ReceiveCallback), newClientEP);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void SendCallback(IAsyncResult result)
    {
        server.EndSendTo(result);
    }

    public static void SendTo(byte[] data, EndPoint clientEP)
    {
        try
        {
            //server.SendTo(data, clientEP);
            server.BeginSendTo(data, 0, data.Length, 0, clientEP, new AsyncCallback(SendCallback), clientEP);
        }
        catch (Exception)
        {
            clientList.Remove(clientEP);
        }
    }

    public static void SendToAll(string data)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(data);

        foreach (var client in clientList)
        {
            SendTo(buffer, client);
        }
    }
}