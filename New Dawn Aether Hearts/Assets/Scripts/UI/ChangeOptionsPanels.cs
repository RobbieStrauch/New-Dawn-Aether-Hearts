using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOptionsPanels : MonoBehaviour
{
    public List<GameObject> optionPanels;

    // Start is called before the first frame update
    void Start()
    {
        optionPanels[0].SetActive(true);
        optionPanels[1].SetActive(false);
        optionPanels[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(GameObject button)
    {
        if (button.name.Contains("Video"))
        {
            optionPanels[0].SetActive(true);
            optionPanels[1].SetActive(false);
            optionPanels[2].SetActive(false);
        }
        if (button.name.Contains("Audio"))
        {
            optionPanels[0].SetActive(false);
            optionPanels[1].SetActive(true);
            optionPanels[2].SetActive(false);
        }
        if (button.name.Contains("Control"))
        {
            optionPanels[0].SetActive(false);
            optionPanels[1].SetActive(false);
            optionPanels[2].SetActive(true);
        }
    }
}
