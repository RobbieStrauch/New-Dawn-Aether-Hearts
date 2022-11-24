using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text text;
    public Image circle;

    private float totalTime = 1200f;
    private float timer = 0f;
    private float seconds = 0f;
    private float minutes = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().isLoaded && timer > 0.1f)
        {
            timer -= Time.deltaTime;
            minutes = Mathf.Floor(timer / 60f);
            seconds = Mathf.RoundToInt(timer % 60f);

            text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            circle.fillAmount = timer / totalTime;
        }
    }
}
