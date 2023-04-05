using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class QuitCheck : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Debug.Log("Quit");

        if (!GameTimer.instance.victory)
        {
            GameTimer.instance.EndgameScore(-100);
        }

        if(ChatClient.instance.client != null)
        {
            if (ChatClient.instance.client.Connected)
            {
                if (ChatClient.instance.player1 != "")
                {
                    ChatClient.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER1$>DEFEAT"));
                }
                if (ChatClient.instance.player2 != "")
                {
                    ChatClient.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>DEFEAT"));
                }

                ChatClient.instance.client.Shutdown(SocketShutdown.Both);
                ChatClient.instance.client.Close();
                ChatClient.instance.thread.Abort();
            }
        }

        if (UnitClient.instance.GetClient().Client.Connected)
        {
            UnitClient.instance.GetClient().Client.Shutdown(SocketShutdown.Both);
            UnitClient.instance.GetClient().Client.Close();
        }
    }
}
