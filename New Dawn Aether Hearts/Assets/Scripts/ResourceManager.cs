using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int gold = 1;
    public int DelayAmount = 1; // Second count
    public int teamB;
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

    int zones1;
    int zones2;
    int zones3;

    protected float Timer;

    void Update()
    {
        zones();

        Timer += Time.deltaTime;

        if (Timer >= DelayAmount)
        {
            Timer = 0f;
            gold++;
            
            if(zones1 >= 1)
            {
                gold += (1 * zones1);
            }

            if(zones2 >= 1)
            {
                gold += (2 * zones2);
            }

            if(zones3 == 1)
            {
                gold += 3;
            }
        }
    }

    private void zones()
    {

    }
}
