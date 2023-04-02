using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class HighscorePlugin : MonoBehaviour
{
    [DllImport("HighscorePlugin")]
    public static extern void SetHighscore(string name, int score);

    [DllImport("HighscorePlugin")]
    public static extern Highscore GetHighscore();

    [DllImport("HighscorePlugin")]
    public static extern void SaveToFile(string name, int score);

    [DllImport("HighscorePlugin")]
    public static extern void StartWriting(string fileName);

    [DllImport("HighscorePlugin")]
    public static extern void EndWriting();

    string path;
    string fn;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath;
        fn = path + "/PlayerHighscore.txt";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Read(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            //values.Add(float.Parse(line));
        }

        streamReader.Close();
    }
}
