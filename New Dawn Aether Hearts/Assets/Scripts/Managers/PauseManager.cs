using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public bool paused = false;
    public bool options = false;
    public Canvas pausePanel;
    public Canvas optionsPanel;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        if (paused == false)
        {
            pausePanel.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }

        PauseCheck();
    }

    public void PauseToggle()
    {
        paused = !paused;
    }

    public void DoOptions()
    {
        options = !options;

        if (options)
        {
            optionsPanel.enabled = true;
        }
    }

    public void PauseCheck()
    {
        if (paused)
        {
            pausePanel.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.enabled = false;
            optionsPanel.enabled = false;
            options = false;
            Time.timeScale = 1;
        }
    }
}
