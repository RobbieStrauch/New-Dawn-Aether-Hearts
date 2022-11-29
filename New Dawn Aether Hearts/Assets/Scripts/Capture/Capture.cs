using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    public Image backCircle;
    public Image frontCircle;

    public Material RedShader;
    public Material BlueShader;

    public float CaptureTime = 5;

    float ACapture;
    float BCapture;

    bool ACaptureStatus;
    bool BCaptureStatus;

    private bool isTriggered = false;
    private string colliderTag = "";

    private bool ATeamFirst = false;
    private bool BTeamFirst = false;

    void Start()
    {
        ACaptureStatus = false;
        BCaptureStatus = false;

        ACapture = 0;
        BCapture = 0;
    }

    void Update()
    {
        if (isTriggered)
        {
            DoCapture();
        }

        if (ACaptureStatus == true)
        {
            tag = "ATeam";
            GetComponent<Renderer>().material = RedShader;
        }

        if (BCaptureStatus == true)
        {
            tag = "BTeam";
            GetComponent<Renderer>().material = BlueShader;
        }

        CaptureStats();
    }

    void DoCapture()
    {
        if (colliderTag == "ATeam")
        {
            if (!ATeamFirst)
            {
                ATeamFirst = true;
            }

            if (ACapture < CaptureTime)
            {
                ACapture += Time.deltaTime;

                if (BCapture > 0)
                {
                    BCapture -= Time.deltaTime;
                }
            }

            else
            {
                ACaptureStatus = true;
                BCaptureStatus = false;
            }
        }

        if (colliderTag == "BTeam")
        {
            if (!BTeamFirst)
            {
                BTeamFirst = true;
            }

            if (BCapture < CaptureTime)
            {
                BCapture += Time.deltaTime;

                if (ACapture > 0)
                {
                    ACapture -= Time.deltaTime;
                }
            }

            else
            {
                ACaptureStatus = false;
                BCaptureStatus = true;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        colliderTag = other.tag;
        isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }

    public void CaptureStats()
    {
        if (ATeamFirst)
        {
            frontCircle.color = Color.red;
            backCircle.color = Color.blue;

            frontCircle.fillAmount = ACapture / CaptureTime;
        }
        if (BTeamFirst)
        {
            frontCircle.color = Color.blue;
            backCircle.color = Color.red;

            frontCircle.fillAmount = BCapture / CaptureTime;
        }
    }
}
