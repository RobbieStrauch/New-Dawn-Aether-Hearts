using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public enum EnemyUnitType
    {
        Scout,
        Ranged,
        Melee,
        Tank
    };

    public EnemyUnitType enemyUnitType;
}
