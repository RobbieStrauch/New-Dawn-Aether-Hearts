using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;

    public List<TMP_Text> leaderboard;
    List<Highscore> highscores = new List<Highscore>();

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

    }

    // Update is called once per frame
    void Update()
    {
        highscores.Sort(SortByScore);

        for (int i = 0; i < leaderboard.Count; i++)
        {
            if (i < highscores.Count)
            {
                leaderboard[i].text = (i+1).ToString() + ") " + highscores[i].name + ": " + highscores[i].score;
            }
            else
            {
                leaderboard[i].text = (i + 1).ToString() + ") ";
            }
        }
    }

    public void AddNewScore(string name, int score)
    {
        highscores.Add(new Highscore(name, score));
    }

    public int SortByScore(Highscore s1, Highscore s2)
    {
        return s2.score.CompareTo(s1.score);
    }
}
