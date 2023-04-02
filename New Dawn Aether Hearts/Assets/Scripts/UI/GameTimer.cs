using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    public TMP_Text text;
    public Image circle;

    public TMP_Text winnerText;
    public GameObject gameOverPanel;

    public bool victory = false;

    private float totalTime = 30f;
    private float timer = 0f;
    private float seconds = 0f;
    private float minutes = 0f;

    private int aZoneCounter = 0;
    private int bZoneCounter = 0;

    private bool notRead = true;
    private static List<string> readLines = new List<string>();

    string path;
    string fn;

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
        path = Application.dataPath;
        fn = path + "/PlayerHighscore.txt";

        timer = totalTime;

        victory = false;
        notRead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((ClientManager.instance.startGame && timer > 0.1f) || ClientManager.instance.gameOver)
        //if (SceneManager.GetActiveScene().isLoaded && timer > 0.1f)
        {
            timer -= Time.deltaTime;
            minutes = Mathf.Floor(timer / 60f);
            seconds = Mathf.RoundToInt(timer % 60f);

            text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            //circle.fillAmount = timer / totalTime;
        }

        if (timer <= 0.1f)
        {
            victory = true;

            foreach (var item in ResourceManager.instance.allZones)
            {
                if (item == 1)
                {
                    aZoneCounter++;
                }
                if (item == 2)
                {
                    bZoneCounter++;
                }
            }

            gameOverPanel.SetActive(true);

            if (aZoneCounter > bZoneCounter)
            {
                if (ClientManager.instance.player1 != "" && ClientManager.instance.client.Connected)
                {
                    winnerText.text = ClientManager.instance.player1 + " Is Victorious!";
                    ClientManager.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER1$>VICTORY"));
                    EndgameScore(100);
                }
                if (ClientManager.instance.player2 != "" && ClientManager.instance.client.Connected)
                {
                    winnerText.text = ClientManager.instance.player2 + " Is Victorious!";
                    ClientManager.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>VICTORY"));
                    EndgameScore(100);
                }
            }
            if (bZoneCounter > aZoneCounter)
            {
                winnerText.text = "Defeat...";
                EndgameScore(-100);
            }
        }

        if (ClientManager.instance.gameOver)
        {
            gameOverPanel.SetActive(true);
            victory = true;

            if (ClientManager.instance.player1 != "" && ClientManager.instance.client.Connected)
            {
                winnerText.text = "Other Player Has Disconnected\n" + ClientManager.instance.player1 + " Is Victorious!";
                ClientManager.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER1$>VICTORY"));
                EndgameScore(100);
            }
            if (ClientManager.instance.player2 != "" && ClientManager.instance.client.Connected)
            {
                winnerText.text = "Other Player Has Disconnected\n" + ClientManager.instance.player2 + " Is Victorious!";
                ClientManager.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>VICTORY"));
                EndgameScore(100);
            }

            //ClientManager.instance.client.Shutdown(SocketShutdown.Both);
            //ClientManager.instance.client.Close();
            //ClientManager.instance.thread.Abort();
        }
    }

    public void EndgameScore(int newScore)
    {
        if (notRead)
        {
            string readLine = "";
            StreamReader streamReader = new StreamReader(fn);
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                if (line.Length > 1)
                {
                    readLine = line;
                    //readLines.Add(line);
                }
            }
            streamReader.Close();

            string[] temp_string = readLine.Split('|');
            Highscore temp_highscore = new Highscore(temp_string[0], int.Parse(temp_string[1]));
            int temp_int = temp_highscore.score + newScore;

            StreamWriter streamWriter = new StreamWriter(fn);
            streamWriter.Write(ChatManager.instance.username + "|" + temp_int + "\n");
            streamWriter.Close();

            notRead = false;
        }
    }
}
