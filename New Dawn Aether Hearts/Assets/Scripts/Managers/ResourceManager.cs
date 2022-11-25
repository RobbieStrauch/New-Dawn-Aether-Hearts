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
    public GameObject A;

    public GameObject N3;

    public GameObject B1_1;
    public GameObject B1_2;
    public GameObject B1_3;
    public GameObject B1_4;
    public GameObject B;

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
        string A1 = A1_1.tag;
        string A2 = A1_2.tag;
        string A3 = A1_3.tag;
        string A4 = A1_4.tag;
        string Ap2 = A.tag;

        string N = N3.tag;

        string B1 = B1_1.tag;
        string B2 = B1_2.tag;
        string B3 = B1_3.tag;
        string B4 = B1_4.tag;
        string Bp2 = B.tag;


        zones(A1, A2, A3, A4, Ap2, N, B1, B2, B3, B4, Bp2);

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

    private void zones(string A1, string A2, string A3, string A4, string A, string N, string B1, string B2, string B3,  string B4, string B)
    {
        //A1
        if(A1 == "ATeam")
        {
            zoneA1 = 1;
        }
        else if(A1 == "BTeam")
        {
            zoneA1 = 2;
        }

        if (A2 == "ATeam")
        {
            zoneA2 = 1;
        }
        else if(A2 == "BTeam")
        {
            zoneA2 = 2;
        }

        if (A3 == "ATeam")
        {
            zoneA3 = 1;
        }
        else if(A3 == "BTeam")
        {
            zoneA3 = 2;
        }

        if (A4 == "ATeam")
        {
            zoneA4 = 1;
        }
        else if(A4 == "BTeam")
        {
            zoneA4 = 2;
        }

        //B1
        if (B1 == "ATeam")
        {
            zoneB1 = 1;
        }
        else if (B1 == "BTeam")
        {
            zoneB1 = 2;
        }

        if (B2 == "ATeam")
        {
            zoneB2 = 1;
        }
        else if (B2 == "BTeam")
        {
            zoneB2 = 2;
        }

        if (B3 == "ATeam")
        {
            zoneB3 = 1;
        }
        else if (B3 == "BTeam")
        {
            zoneB3 = 2;
        }

        if (B4 == "ATeam")
        {
            zoneB4 = 1;
        }
        else if (B4 == "BTeam")
        {
            zoneB4 = 2;
        }

        //2
        if (A == "ATeam")
        {
            zoneA = 1;
        }
        else if(A == "BTeam")
        {
            zoneB = 2;
        }

        if(B == "ATeam")
        {
            zoneB = 1;
        }
        else if(B == "BTeam")
        {
            zoneB = 2;
        }

        //3
        if(N == "ATeam")
        {
            zone3 = 1;
        }
        else if(N == "BTeam")
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
