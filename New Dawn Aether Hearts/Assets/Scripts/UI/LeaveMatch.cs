using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveMatch : MonoBehaviour
{
    public void Press()
    {
        ClientManager.instance.client.Shutdown(SocketShutdown.Both);
        ClientManager.instance.client.Close();
        ClientManager.instance.thread.Abort();
        SceneManager.LoadScene("MenuScene");
    }
}
