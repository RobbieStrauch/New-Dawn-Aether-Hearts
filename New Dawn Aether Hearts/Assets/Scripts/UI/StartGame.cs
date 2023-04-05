using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Press()
    {
        HighscoreClient.instance.client.Shutdown(SocketShutdown.Both);
        HighscoreClient.instance.client.Close();
        HighscoreClient.instance.thread.Abort();
        SceneManager.LoadScene("CurrentScene");
    }

    public void tutorial()
    {
        HighscoreClient.instance.client.Shutdown(SocketShutdown.Both);
        HighscoreClient.instance.client.Close();
        HighscoreClient.instance.thread.Abort();
        SceneManager.LoadScene("Cinema Test");
    }
}
