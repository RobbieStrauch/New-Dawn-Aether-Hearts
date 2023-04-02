using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Press()
    {
        HighscoreClient.instance.client.Shutdown(SocketShutdown.Both);
        HighscoreClient.instance.client.Close();
        HighscoreClient.instance.thread.Abort();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}