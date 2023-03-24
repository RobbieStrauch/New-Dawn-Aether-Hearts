using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private int damage = 20;

    public enum SwordType
    {
        ATeam,
        BTeam
    };

    public SwordType swordType;

    private void OnTriggerEnter(Collider other)
    {
        if (swordType == SwordType.ATeam)
        {
            if (other.gameObject.GetComponent<UnitHealth>() && other.gameObject.tag == "BTeam")
            {
                other.gameObject.GetComponent<UnitHealth>().DecreaseHealth(damage);
            }
        }
        if (swordType == SwordType.BTeam)
        {
            if (other.gameObject.GetComponent<UnitHealth>() && other.gameObject.tag == "ATeam")
            {
                other.gameObject.GetComponent<UnitHealth>().DecreaseHealth(damage);
            }
        }
    }

    public void ChangeDamage(int value)
    {
        damage = value;
    }
}
