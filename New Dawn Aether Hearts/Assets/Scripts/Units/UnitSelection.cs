using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference from: https://www.youtube.com/watch?v=vAVi04mzeKk&t=2s

public class UnitSelection : MonoBehaviour
{
    public static UnitSelection instance;

    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelectedList = new List<GameObject>();

    public Material unselectedMaterial;
    public Material selectedMaterial;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void ClickSelect(GameObject addUnit)
    {
        DeselectAll();
        unitSelectedList.Add(addUnit);
        addUnit.GetComponent<Renderer>().material = selectedMaterial;
        addUnit.transform.GetComponent<StateCycle>().isSelected = true;
    }

    public void ContinueClickSelect(GameObject addUnit)
    {
        if (!unitSelectedList.Contains(addUnit))
        {
            unitSelectedList.Add(addUnit);
            addUnit.GetComponent<Renderer>().material = selectedMaterial;
            addUnit.transform.GetComponent<StateCycle>().isSelected = true;
        }
        else
        {
            addUnit.transform.GetComponent<StateCycle>().isSelected = false;
            addUnit.GetComponent<Renderer>().material = unselectedMaterial;
            unitSelectedList.Remove(addUnit);
        }
    }

    public void DragSelect(GameObject addUnit)
    {
        if (!unitSelectedList.Contains(addUnit))
        {
            unitSelectedList.Add(addUnit);
            addUnit.GetComponent<Renderer>().material = selectedMaterial;
            addUnit.transform.GetComponent<StateCycle>().isSelected = true;
        }
    }

    public void DeselectAll()
    {
        foreach (GameObject unit in unitSelectedList)
        {
            unit.transform.GetComponent<StateCycle>().isSelected = false;
            unit.GetComponent<Renderer>().material = unselectedMaterial;
        }
        unitSelectedList.Clear();
    }

    public void Deselect(GameObject removeUnit)
    {
        if (unitSelectedList.Contains(removeUnit))
        {
            removeUnit.transform.GetComponent<StateCycle>().isSelected = false;
            removeUnit.GetComponent<Renderer>().material = unselectedMaterial;
            unitSelectedList.Remove(removeUnit);
        }
    }
}
