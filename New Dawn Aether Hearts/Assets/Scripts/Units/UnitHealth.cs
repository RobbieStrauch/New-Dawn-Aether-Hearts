using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    public float health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetHealth() <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseHealth(float newValue)
    {
        health -= newValue;
    }

    public void IncreaseHealth(float newValue)
    {
        health += newValue;
    }

    public float GetHealth()
    {
        return health;
    }
}
