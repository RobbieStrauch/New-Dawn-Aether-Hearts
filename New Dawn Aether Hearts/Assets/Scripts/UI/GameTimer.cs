using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Net.Sockets;

public class GameTimer : MonoBehaviour
{
    public TMP_Text text;
    public Image circle;

    public TMP_Text winnerText;
    public GameObject gameOverPanel;

    private float totalTime = 30f;
    private float timer = 0f;
    private float seconds = 0f;
    private float minutes = 0f;

    private int aZoneCounter = 0;
    private int bZoneCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClientManager.instance.startGame && timer > 0.1f)
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
                }
                if (ClientManager.instance.player2 != "" && ClientManager.instance.client.Connected)
                {
                    winnerText.text = ClientManager.instance.player2 + " Is Victorious!";
                    ClientManager.instance.client.Send(Encoding.ASCII.GetBytes("$<PLAYER2$>VICTORY"));
                }
            }
            if (bZoneCounter > aZoneCounter)
            {
                winnerText.text = "Defeat...";
            }
        }
    }
}
