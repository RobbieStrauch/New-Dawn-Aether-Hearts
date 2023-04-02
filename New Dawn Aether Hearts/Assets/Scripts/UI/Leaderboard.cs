using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public List<TMP_Text> leaderboard;
    List<Highscore> highscores = new List<Highscore>();

    // Start is called before the first frame update
    void Start()
    {
        AddNewScore("John", 4500);
        AddNewScore("Max", 5520);
        AddNewScore("Dave", 380);
        AddNewScore("Steve", 6654);
        AddNewScore("Mike", 11021);
        AddNewScore("Kaelum", 3535);
        AddNewScore("William", 2222);
        AddNewScore("Josh", 5624);
        AddNewScore("Robbie", 50000);
        AddNewScore("Evrett", 6969);
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
                leaderboard[i].text = "";
            }
        }
    }

    void AddNewScore(string name, int score)
    {
        highscores.Add(new Highscore(name, score));
    }

    int SortByScore(Highscore s1, Highscore s2)
    {
        return s2.score.CompareTo(s1.score);
    }
}
