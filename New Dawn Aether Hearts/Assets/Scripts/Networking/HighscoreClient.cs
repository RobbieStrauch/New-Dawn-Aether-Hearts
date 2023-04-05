using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System;
using UnityEngine;
using System.Net;
using System.Text;
using System.IO;
using UnityEngine.SceneManagement;

public class HighscoreClient : MonoBehaviour
{
    public static HighscoreClient instance;

    public int port = 8887;
    public bool menuOpen = true;
    public bool connected = false;
    public static List<string> readLines = new List<string>();

    public Socket client;
    public Thread thread;

    static byte[] buffer = new byte[512];
    static bool receivedData = false;
    static int receivedCopy;

    public static string currentLine;

    static string path;
    static string fn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        menuOpen = true;
        connected = false;

        path = Application.dataPath;
        fn = path + "/PlayerHighscore.txt";
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (!connected)
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(ip, port);
                connected = true;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        if (connected)
        {
            thread = new Thread(o => ReceiveData((Socket)o));
            thread.Start(client);

            if (menuOpen)
            {
                Read(fn);
                byte[] sent = Encoding.ASCII.GetBytes("{<HIGHSCORE>}|" + currentLine);
                client.Send(sent);
                menuOpen = false;
            }
        }

        if (receivedData)
        {
            byte[] data = new byte[receivedCopy];
            Array.Copy(buffer, data, receivedCopy);
            string message = Encoding.ASCII.GetString(data);

            if (client.Connected)
            {
                if (message.Contains("{<NEWHIGHSCORE>}"))
                {
                    string[] temp_string = message.Split('|');
                    for (int i = 0; i < int.Parse(temp_string[1]); i++)
                    {
                        Highscore temp_highscore = new Highscore(temp_string[2 + (i * 2)], int.Parse(temp_string[3 + (i * 2)]));
                        foreach (var item in Leaderboard.instance.highscores)
                        {
                            if (item.name != temp_highscore.name || item.name != "Temporary")
                            {
                                Leaderboard.instance.AddNewScore(temp_highscore.name, temp_highscore.score);
                            }
                        }
                    }
                }
            }

            receivedCopy = 0;
            receivedData = false;
        }
    }

    private static void ReceiveCallback(IAsyncResult result)
    {
        Socket socket = (Socket)result.AsyncState;
        int receive = socket.EndReceive(result);
        receivedCopy = receive;
        receivedData = true;
        socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);
    }

    public static void ReceiveData(Socket socket)
    {
        if (socket.Connected)
        {
            socket.BeginReceive(buffer, 0, buffer.Length, 0, new AsyncCallback(ReceiveCallback), socket);
        }
    }

    public static void Read(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            currentLine = line;
        }

        streamReader.Close();
    }
}
