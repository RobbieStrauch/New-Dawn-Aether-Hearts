using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOptions : MonoBehaviour
{
    public bool panelOn;
    Canvas panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (panel)
        {
            panel.enabled = true;
        }
        else
        {
            panel.enabled = false;
        }
    }

    public void ButtonPressed()
    {
        panelOn = !panelOn;
    }
}
