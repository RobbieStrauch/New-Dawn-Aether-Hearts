using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture : MonoBehaviour
{
    public Material RedShader;
    public Material BlueShader;

    public float CaptureTime = 5;

    float ACapture;
    float BCapture;

    float APoints = 0;
    float BPoints = 0;

    bool ACaptureStatus;
    bool BCaptureStatus;

    private bool isTriggered = false;
    private string colliderTag = "";

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
            GetComponent<Renderer>().material = RedShader;

            APoints += Time.deltaTime;

            string score = "A score is " + APoints;
            Debug.Log(score);
        }

        if (BCaptureStatus == true)
        {
            GetComponent<Renderer>().material = BlueShader;

            BPoints += Time.deltaTime;

            string score = "B score is " + BPoints;
            Debug.Log(score);
        }
    }

    void DoCapture()
    {
        if (colliderTag == "ATeam")
        {

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
}
