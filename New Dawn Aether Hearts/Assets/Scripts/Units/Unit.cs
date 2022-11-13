using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitSelection.instance.unitList.Add(this.gameObject);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        UnitSelection.instance.unitList.Remove(this.gameObject);
    }
}