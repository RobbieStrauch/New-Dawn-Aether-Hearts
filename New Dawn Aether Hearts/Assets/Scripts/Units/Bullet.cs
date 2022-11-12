using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<UnitHealth>())
        {
            other.gameObject.GetComponent<UnitHealth>().DecreaseHealth(5);
        }

        Destroy(gameObject);
    }
}
