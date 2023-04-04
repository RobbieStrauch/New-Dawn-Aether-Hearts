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

        if(ClientManager.instance.client != null)
        {
            if (ClientManager.instance.client.Connected)
            {
                if (ClientManager.instance.player1 != "")
                {
                    ClientManager.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER1$>DEFEAT"));
                }
                if (ClientManager.instance.player2 != "")
                {
                    ClientManager.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>DEFEAT"));
                }

                ClientManager.instance.client.Shutdown(SocketShutdown.Both);
                ClientManager.instance.client.Close();
                ClientManager.instance.thread.Abort();
            }
        }

        if (UnitClient.instance.GetClient().Client.Connected)
        {
            UnitClient.instance.GetClient().Client.Shutdown(SocketShutdown.Both);
            UnitClient.instance.GetClient().Client.Close();
        }
    }
}
