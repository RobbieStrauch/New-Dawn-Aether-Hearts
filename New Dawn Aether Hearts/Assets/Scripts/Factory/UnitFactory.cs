using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;
using TMPro;

public class UnitFactory : MonoBehaviour
{
    public GameObject prefab1;
    public GameObject prefab2;

    public GameObject buttonPanel;
    public GameObject buttonPrefab;

    List<_Factory> placedUnits;
    // Start is called before the first frame update
    void Start()
    {
        var unitTypes = Assembly.GetAssembly(typeof(_Factory)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(_Factory)));

        placedUnits = new List<_Factory>();

        foreach (var type in unitTypes)
        {
            var tempType = Activator.CreateInstance(type) as _Factory;
            placedUnits.Add(tempType);
        }

        ButtonPanel();
    }

    public _Factory Get_Factory(string uniType)
    {
        foreach (_Factory uni in placedUnits)
        {
            if (uni.Name == uniType)
            {
                //Debug.Log("Unit Found");
                var target = Activator.CreateInstance(uni.GetType()) as _Factory;

                return target;
            }
        }
        return null;
    }

    void ButtonPanel()
    {
        foreach (_Factory uni in placedUnits)
        {
            var button = Instantiate(buttonPrefab);
            button.transform.SetParent(buttonPanel.transform);
            button.gameObject.name = uni.Name + " Button";
            button.GetComponentInChildren<TextMeshProUGUI>().text = uni.Name;
        }
    }
}
