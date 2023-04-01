using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Net;
using System.Text;

public class ClientManager : MonoBehaviour
{
    public static ClientManager instance;

    public string player1 = "";
    public string player2 = "";

    public int port = 8888;
    public string username;
    public string code;
    public bool usernameInput;
    public bool codeInput;
    public bool player1Set;
    public bool player2Set;
    public bool isTCPConnected;
    public bool isConnecting;
    public bool startGame;
    public bool gameOver;

    public GameObject mainCamera;
    public GameObject vision;
    public Transform blueCameraPosition;
    public Transform redCameraPosition;
    public Transform visionBluePosition;
    public Transform visionRedPosition;
    public Canvas multiplayerCanvas;
    public Canvas gameplayCanvas;
    public Canvas editorCanvas;
    public GameObject lobbyPanel;
    public GameObject usernamePanel;
    public GameObject codePanel;
    public GameObject teamPanel;
    public TMP_InputField usernameInputField;
    public TMP_InputField codeInputField;
    public TMP_Text blueTeamText;
    public TMP_Text redTeamText;

    public Socket client;
    public Thread thread;

    static byte[] buffer = new byte[512];
    static bool receivedData = false;
    static int receivedCopy;

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
        usernamePanel.SetActive(true);
        lobbyPanel.SetActive(false);
        codePanel.SetActive(false);
        teamPanel.SetActive(false);
        usernameInput = false;
        isTCPConnected = false;
        isConnecting = false;
        startGame = false;
        gameOver = false;
        editorCanvas.enabled = false;
        gameplayCanvas.enabled = false;
        multiplayerCanvas.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (multiplayerCanvas.enabled)
        {
            if (usernamePanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return) && usernameInputField.text != string.Empty)
                {
                    username = usernameInputField.text;
                    usernameInput = true;
                }
                if (ChatManager.instance.username != "")
                {
                    lobbyPanel.SetActive(true);
                    usernamePanel.SetActive(false);
                }
            }
            if (codePanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Return) && codeInputField.text != string.Empty)
                {
                    code = codeInputField.text;
                    codeInput = true;
                }
                if (code == port.ToString() && codeInput)
                {
                    redTeamText.text = ChatManager.instance.username;
                    player2 = ChatManager.instance.username;
                    player2Set = true;
                    isConnecting = true;
                    teamPanel.SetActive(true);
                    codePanel.SetActive(false);
                }
                else
                {
                    code = string.Empty;
                    codeInput = false;
                }
            }
            if (!isConnecting && codeInput)
            {
                codePanel.SetActive(true);
                teamPanel.SetActive(false);
                redTeamText.text = "Waiting On Player...";
                player2 = "";
                player2Set = false;
            }
        }

        if (!isTCPConnected && isConnecting)
        {
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(ip, port);
                isTCPConnected = true;
            }
            catch (Exception)
            {
                isConnecting = false;
            }
        }
        if (isTCPConnected && isConnecting)
        {
            thread = new Thread(o => ReceiveData((Socket)o));
            thread.Start(client);

            if (player1 != "" && player1Set)
            {
                client.Send(Encoding.ASCII.GetBytes("$<PLAYER1$>"));
                mainCamera.transform.position = blueCameraPosition.position;
                vision.transform.position = visionBluePosition.position;
                player1Set = false;
            }
            if (player2 != "" && player2Set)
            {
                client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>"));
                mainCamera.transform.position = redCameraPosition.position;
                vision.transform.position = visionRedPosition.position;
                player2Set = false;
            }

            if (ChatManager.instance.sentMessage)
            {
                byte[] data = Encoding.ASCII.GetBytes(ChatManager.instance.usernameStructure + ChatManager.instance.inputMessage);
                client.Send(data);
                ChatManager.instance.sentMessage = false;
            }

            if (receivedData)
            {
                byte[] data = new byte[receivedCopy];
                Array.Copy(buffer, data, receivedCopy);
                string message = Encoding.ASCII.GetString(data);

                if (client.Connected)
                {
                    //Debug.Log(message);

                    if (message.Contains("$<STARTGAME>$"))
                    {
                        startGame = true;
                        editorCanvas.enabled = true;
                        gameplayCanvas.enabled = true;
                        multiplayerCanvas.enabled = false;
                    }
                    if (message.Contains("$<PLAYER1$>DEFEAT") || message.Contains("$<PLAYER2$>DEFEAT"))
                    {
                        gameOver = true;
                    }
                    if (message.Length > 1)
                    {
                        if (message[0] == '[')
                        {
                            ChatManager.instance.SendChatMessage(message, Message.MessageType.OtherMessage);
                        }
                    }
                }

                receivedCopy = 0;
                receivedData = false;
            }
        }
    }

    public void CreateLobby()
    {
        teamPanel.SetActive(true);
        blueTeamText.text = ChatManager.instance.username;
        player1 = ChatManager.instance.username;
        player1Set = true;
        isConnecting = true;
        lobbyPanel.SetActive(false);
    }

    public void JoinLobby()
    {
        codePanel.SetActive(true);
        lobbyPanel.SetActive(false);
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
}
