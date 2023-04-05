using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum UnitType
    {
        Scout,
        Ranged,
        Melee,
        Tank
    };

    public UnitType unitType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnDestroy()
    {
        UnitSelection.instance.unitList.Remove(this.gameObject);
    }

    private void OnEnable()
    {
        UnitSelection.instance.unitList.Add(this.gameObject);
    }

    private void OnDisable()
    {
        UnitSelection.instance.unitList.Remove(this.gameObject);
        UnitSelection.instance.unitSelectedList.Remove(this.gameObject);
    }
}
