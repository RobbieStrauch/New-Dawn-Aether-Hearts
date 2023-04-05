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
        if ((ChatClient.instance.startGame && timer > 0.1f) || ChatClient.instance.gameOver)
        {
            timer -= Time.deltaTime;
            minutes = Mathf.Floor(timer / 60f);
            seconds = Mathf.RoundToInt(timer % 60f);

            text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
                if (ChatClient.instance.player1 != "" && ChatClient.instance.client.Connected)
                {
                    winnerText.text = ChatClient.instance.player1 + " Is Victorious!";
                    ChatClient.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER1$>VICTORY"));
                    EndgameScore(100);
                }
                if (ChatClient.instance.player2 != "" && ChatClient.instance.client.Connected)
                {
                    winnerText.text = ChatClient.instance.player2 + " Is Victorious!";
                    ChatClient.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>VICTORY"));
                    EndgameScore(100);
                }
            }
            if (bZoneCounter > aZoneCounter)
            {
                winnerText.text = "Defeat...";
                EndgameScore(-100);
            }
        }

        if (ChatClient.instance.gameOver)
        {
            gameOverPanel.SetActive(true);
            victory = true;

            if (ChatClient.instance.player1 != "" && ChatClient.instance.client.Connected)
            {
                winnerText.text = "Other Player Has Disconnected\n" + ChatClient.instance.player1 + " Is Victorious!";
                ChatClient.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER1$>VICTORY"));
                EndgameScore(100);
            }
            if (ChatClient.instance.player2 != "" && ChatClient.instance.client.Connected)
            {
                winnerText.text = "Other Player Has Disconnected\n" + ChatClient.instance.player2 + " Is Victorious!";
                ChatClient.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>VICTORY"));
                EndgameScore(100);
            }
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
