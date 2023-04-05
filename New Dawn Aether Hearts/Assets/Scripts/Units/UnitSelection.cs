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
        addUnit.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        addUnit.transform.GetComponent<StateCycle>().isSelected = true;

        for (int i = 0; i < unitSelectedList.Count; i++)
        {
            unitSelectedList[i].GetComponent<StateCycle>().priority = i;
        }
    }

    public void ContinueClickSelect(GameObject addUnit)
    {
        if (!unitSelectedList.Contains(addUnit))
        {
            unitSelectedList.Add(addUnit);
            addUnit.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            addUnit.transform.GetComponent<StateCycle>().isSelected = true;
        }
        else
        {
            addUnit.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            addUnit.transform.GetComponent<StateCycle>().isSelected = false;
            unitSelectedList.Remove(addUnit);
        }

        for (int i = 0; i < unitSelectedList.Count; i++)
        {
            unitSelectedList[i].GetComponent<StateCycle>().priority = i;
        }
    }

    public void DragSelect(GameObject addUnit)
    {
        if (!unitSelectedList.Contains(addUnit))
        {
            unitSelectedList.Add(addUnit);
            addUnit.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            addUnit.transform.GetComponent<StateCycle>().isSelected = true;
        }

        for (int i = 0; i < unitSelectedList.Count; i++)
        {
            unitSelectedList[i].GetComponent<StateCycle>().priority = i;
        }
    }

    public void DeselectAll()
    {
        foreach (GameObject unit in unitSelectedList)
        {
            unit.transform.GetComponent<StateCycle>().isSelected = false;
            unit.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        unitSelectedList.Clear();
    }

    public void Deselect(GameObject removeUnit)
    {
        if (unitSelectedList.Contains(removeUnit))
        {
            removeUnit.transform.GetComponent<StateCycle>().isSelected = false;
            removeUnit.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            unitSelectedList.Remove(removeUnit);
        }
    }
}
