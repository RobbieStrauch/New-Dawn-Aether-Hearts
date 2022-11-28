using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<UnitHealth>())
        {
            other.gameObject.GetComponent<UnitHealth>().DecreaseHealth(20);
        }
    }
}
