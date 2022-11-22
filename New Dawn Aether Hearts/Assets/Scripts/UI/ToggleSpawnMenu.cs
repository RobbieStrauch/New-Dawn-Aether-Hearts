using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleSpawnMenu : MonoBehaviour
{
    public GameObject unitPanel;
    public Transform startPosition;
    public Transform endPosition;

    private bool isPressed = false;
    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            isActive = !isActive;
            unitPanel.SetActive(isActive);
            if (isActive)
            {
                gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
                gameObject.transform.position = startPosition.position;
            }
            else
            {
                gameObject.transform.rotation = new Quaternion(0f, 0f, 180f, 1f);
                gameObject.transform.position = endPosition.position;
            }
            Reset();
        }
    }

    private void Reset()
    {
        isPressed = false;
    }

    public void Switch()
    {
        if (!isPressed)
        {
            isPressed = true;
        }
    }
}
