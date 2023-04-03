using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;

public class UnitClient : MonoBehaviour
{
    public static UnitClient instance;

    public static byte[] buffer = new byte[512];
    public static UdpClient client = new UdpClient();

    private static bool receivedData = false;
    private static byte[] bytes = new byte[512];
    private static bool UDPConnected = false;

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
        buffer = new byte[512];
        client = new UdpClient();
        receivedData = false;
        bytes = new byte[512];
        UDPConnected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!UDPConnected)
        {
            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), 8889);
                //ClientManager.instance.isUDPConnected = true;
                UDPConnected = true;
                byte[] data = Encoding.ASCII.GetBytes("UDP Client Connected");
                client.Send(data, data.Length);
                client.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
        if (UDPConnected)
        {
            if (receivedData)
            {
                string receivedString = Encoding.ASCII.GetString(bytes);

                Debug.Log(receivedString);

                string[] data = receivedString.Split('|');

                if (data.Length == 9 && data[0] != client.Client.LocalEndPoint.ToString() && data[2] == "Activate")
                {
                    UnitObjectPooler.instance.SpawnFromPool("TeamB " + data[1].Substring(6, data[1].Length - 7));
                    GameObject unit = GameObject.Find("TeamB " + data[1].Substring(6, data[1].Length - 6));
                    if (unit != null)
                    {
                        unit.transform.position = new Vector3(float.Parse(data[3]), float.Parse(data[4]), float.Parse(data[5]));
                        unit.transform.eulerAngles = new Vector3(float.Parse(data[6]), float.Parse(data[7]), float.Parse(data[8]));
                    }
                }

                if (data.Length == 8 && data[0] != client.Client.LocalEndPoint.ToString())
                {
                    GameObject unit = GameObject.Find("TeamB " + data[1].Substring(6, data[1].Length - 6));
                    if (unit != null)
                    {
                        unit.transform.position = new Vector3(float.Parse(data[2]), float.Parse(data[3]), float.Parse(data[4]));
                        unit.transform.eulerAngles = new Vector3(float.Parse(data[5]), float.Parse(data[6]), float.Parse(data[7]));
                    }
                }

                receivedData = false;
            }
        }
    }

    private static void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receive = client.EndReceive(result, ref remoteEndPoint);
            bytes = receive;

            receivedData = true;

            client.BeginReceive(new AsyncCallback(ReceiveCallback), null);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public UdpClient GetClient()
    {
        return client;
    }
}