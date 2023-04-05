using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitHealth : MonoBehaviour
{
    public float health = 100f;
    private float originalHealth;

    // Start is called before the first frame update
    void Start()
    {
        originalHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetHealth() <= 0f)
        {
            if (gameObject.GetComponent<Unit>())
            {
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Scout)
                {
                    UnitTracker.instance.scoutCount--;
                }
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Ranged)
                {
                    UnitTracker.instance.rangedCount--;
                }
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Melee)
                {
                    UnitTracker.instance.meleeCount--;
                }
                if (gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Tank)
                {
                    UnitTracker.instance.tankCount--;
                }
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
            health = originalHealth;
            gameObject.SetActive(false);
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
