using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTracker : MonoBehaviour
{
    public static UnitTracker instance;

    public int scoutCount;
    public int rangedCount;
    public int meleeCount;
    public int tankCount;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoutCount = 0;
        rangedCount = 0;
        meleeCount = 0;
        tankCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
