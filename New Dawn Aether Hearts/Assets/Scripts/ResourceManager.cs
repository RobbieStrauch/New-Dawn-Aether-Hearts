using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int Agold;
    public int Bgold;
    public int DelayAmount;

    public GameObject A1_1;
    public GameObject A1_2;
    public GameObject A1_3;
    public GameObject A1_4;
    public GameObject A2;

    public GameObject N3;

    public GameObject B1_1;
    public GameObject B1_2;
    public GameObject B1_3;
    public GameObject B1_4;
    public GameObject B2;

    int zoneA1 = 0;
    int zoneA2 = 0;
    int zoneA3 = 0;
    int zoneA4 = 0;
    int zoneA = 0;

    int zone3 = 0;

    int zoneB1 = 0;
    int zoneB2 = 0;
    int zoneB3 = 0;
    int zoneB4 = 0;
    int zoneB = 0;

    public Material RedShader;
    public Material BlueShader;

    protected float Timer;

    void Update()
    {
        zones();

        Timer += Time.deltaTime;

        if (Timer >= DelayAmount)
        {
            Timer = 0f;
            Agold++;
            Bgold++;

            GoldA();
            GoldB();
        }
    }

    private void zones()
    {
        //A1
        if(A1_1.GetComponent<Renderer>().material == RedShader)
        {
            zoneA1 = 1;
        }
        else if(A1_1.GetComponent<Renderer>().material == BlueShader)
        {
            zoneA1 = 2;
        }

        if (A1_2.GetComponent<Renderer>().material == RedShader)
        {
            zoneA2 = 1;
        }
        else if(A1_2.GetComponent<Renderer>().material == BlueShader)
        {
            zoneA2 = 2;
        }

        if (A1_3.GetComponent<Renderer>().material == RedShader)
        {
            zoneA3 = 1;
        }
        else if(A1_3.GetComponent<Renderer>().material == BlueShader)
        {
            zoneA3 = 2;
        }

        if (A1_4.GetComponent<Renderer>().material == RedShader)
        {
            zoneA4 = 1;
        }
        else if(A1_4.GetComponent<Renderer>().material == BlueShader)
        {
            zoneA4 = 2;
        }

        //B1
        if (B1_1.GetComponent<Renderer>().material == RedShader)
        {
            zoneB1 = 1;
        }
        else if (B1_1.GetComponent<Renderer>().material == BlueShader)
        {
            zoneB1 = 2;
        }

        if (B1_2.GetComponent<Renderer>().material == RedShader)
        {
            zoneB2 = 1;
        }
        else if (B1_2.GetComponent<Renderer>().material == BlueShader)
        {
            zoneB2 = 2;
        }

        if (B1_3.GetComponent<Renderer>().material == RedShader)
        {
            zoneB3 = 1;
        }
        else if (B1_3.GetComponent<Renderer>().material == BlueShader)
        {
            zoneB3 = 2;
        }

        if (B1_4.GetComponent<Renderer>().material == RedShader)
        {
            zoneB4 = 1;
        }
        else if (B1_4.GetComponent<Renderer>().material == BlueShader)
        {
            zoneB4 = 2;
        }

        //2
        if (A2.GetComponent<Renderer>().material == RedShader)
        {
            zoneA = 1;
        }
        else if(A2.GetComponent<Renderer>().material == BlueShader)
        {
            zoneB = 2;
        }

        if(B2.GetComponent<Renderer>().material == RedShader)
        {
            zoneB = 1;
        }
        else if(B2.GetComponent<Renderer>().material == BlueShader)
        {
            zoneB = 2;
        }

        //3
        if(N3.GetComponent<Renderer>().material == RedShader)
        {
            zone3 = 1;
        }
        else if(N3.GetComponent<Renderer>().material == BlueShader)
        {
            zone3 = 2;
        }
    }

    private void GoldA()
    {
        //A
        if (zoneA1 == 1)
        {
            Agold += 1;
        }

        if (zoneA2 == 1)
        {
            Agold += 1;
        }

        if (zoneA3 == 1)
        {
            Agold += 1;
        }

        if (zoneA4 == 1)
        {
            Agold += 1;
        }

        //B
        if (zoneB1 == 1)
        {
            Agold += 1;
        }

        if (zoneB2 == 1)
        {
            Agold += 1;
        }

        if (zoneB3 == 1)
        {
            Agold += 1;
        }

        if (zoneB4 == 1)
        {
            Agold += 1;
        }

        //2
        if (zoneA == 1)
        {
            Agold += 2;
        }

        if (zoneB == 1)
        {
            Agold += 2;
        }

        //3
        if (zone3 == 1)
        {
            Agold += 3;
        }
    }

    private void GoldB()
    {
        //A
        if (zoneA1 == 2)
        {
            Bgold += 1;
        }

        if (zoneA2 == 2)
        {
            Bgold += 1;
        }

        if (zoneA3 == 2)
        {
            Bgold += 1;
        }

        if (zoneA4 == 2)
        {
            Bgold += 1;
        }

        //B
        if (zoneB1 == 2)
        {
            Bgold += 1;
        }

        if (zoneB2 == 2)
        {
            Bgold += 1;
        }

        if (zoneB3 == 2)
        {
            Bgold += 1;
        }

        if (zoneB4 == 2)
        {
            Bgold += 1;
        }

        //2
        if (zoneA == 2)
        {
            Bgold += 2;
        }

        if (zoneB == 2)
        {
            Bgold += 2;
        }
        
        //3
        if (zone3 == 2)
        {
            Bgold += 3;
        }
    }
}
