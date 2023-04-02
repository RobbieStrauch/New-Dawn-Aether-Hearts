using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore
{
    public string name;
    public int score;

    public Highscore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
