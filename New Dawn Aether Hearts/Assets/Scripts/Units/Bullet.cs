using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 5;

    public enum BulletType
    {
        ATeam,
        BTeam
    };

    public BulletType bulletType;

    private void OnCollisionEnter(Collision other)
    {
        if (bulletType == BulletType.ATeam)
        {
            if (other.gameObject.GetComponent<UnitHealth>() && other.gameObject.tag == "BTeam")
            {
                other.gameObject.GetComponent<UnitHealth>().DecreaseHealth(damage);
            }
        }
        if (bulletType == BulletType.BTeam)
        {
            if (other.gameObject.GetComponent<UnitHealth>() && other.gameObject.tag == "ATeam")
            {
                other.gameObject.GetComponent<UnitHealth>().DecreaseHealth(damage);
            }
        }

        Destroy(gameObject);
    }

    public void ChangeDamage(int value)
    {
        damage = value;
    }
}
