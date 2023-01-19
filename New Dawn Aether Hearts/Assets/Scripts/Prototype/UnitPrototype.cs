using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitPrototype : MonoBehaviour
{
    public List<UnitData> allData;
    public List<GameObject> unitPanels;

    public int scoutCount = 0;
    public int rangedCount = 0;
    public int meleeCount = 0;
    public int tankCount = 0;

    Scout scout;
    Ranged ranged;
    Melee melee;
    Tank tank;

    // Start is called before the first frame update
    void Start()
    {
        scout = new Scout(allData[0]._prefab, allData[0]._cost);
        ranged = new Ranged(allData[1]._prefab, allData[1]._cost);
        melee = new Melee(allData[2]._prefab, allData[2]._cost);
        tank = new Tank(allData[3]._prefab, allData[3]._cost);
    }

    public void Spawner(GameObject button)
    {
        var b = button.GetComponentInChildren<TextMeshProUGUI>();
        ResourceManager resource = ResourceManager.instance;

        CheckUnitCount();

        if (button.gameObject.transform.parent.name == "ScoutPanel" && resource.Agold >= scout.Cost() && scoutCount < 10)
        {
            resource.Agold -= scout.Cost();
            EditorManager.instance.item = scout.Clone().Spawn();
            EditorManager.instance.instantiated = true;
        }
        if (button.gameObject.transform.parent.name == "RangedPanel" && resource.Agold >= ranged.Cost() && rangedCount < 6)
        {
            resource.Agold -= ranged.Cost();
            EditorManager.instance.item = ranged.Clone().Spawn();
            EditorManager.instance.instantiated = true;
        }
        if (button.gameObject.transform.parent.name == "MeleePanel" && resource.Agold >= melee.Cost() && meleeCount < 4)
        {
            resource.Agold -= melee.Cost();
            EditorManager.instance.item = melee.Clone().Spawn();
            EditorManager.instance.instantiated = true;
        }
        if (button.gameObject.transform.parent.name == "TankPanel" && resource.Agold >= tank.Cost() && tankCount < 2)
        {
            resource.Agold -= tank.Cost();
            EditorManager.instance.item = tank.Clone().Spawn();
            EditorManager.instance.instantiated = true;
        }
    }

    public void OpenPanel(GameObject button)
    {
        var b = button.GetComponentInChildren<TextMeshProUGUI>();

        switch (b.text)
        {
            case "Scout":
                unitPanels[0].SetActive(true);
                unitPanels[1].SetActive(false);
                unitPanels[2].SetActive(false);
                unitPanels[3].SetActive(false);
                break;
            case "Ranged":
                unitPanels[0].SetActive(false);
                unitPanels[1].SetActive(true);
                unitPanels[2].SetActive(false);
                unitPanels[3].SetActive(false);
                break;
            case "Melee":
                unitPanels[0].SetActive(false);
                unitPanels[1].SetActive(false);
                unitPanels[2].SetActive(true);
                unitPanels[3].SetActive(false);
                break;
            case "Tank":
                unitPanels[0].SetActive(false);
                unitPanels[1].SetActive(false);
                unitPanels[2].SetActive(false);
                unitPanels[3].SetActive(true);
                break;
            default:
                break;
        }
    }

    void CheckUnitCount()
    {
        scoutCount = 0;
        rangedCount = 0;
        meleeCount = 0;
        tankCount = 0;

        UnitSelection unitSelection = UnitSelection.instance;

        foreach (var item in unitSelection.unitList)
        {
            if (item.name.Contains("Scout"))
            {
                scoutCount++;
            }
            if (item.name.Contains("Ranged"))
            {
                rangedCount++;
            }
            if (item.name.Contains("Melee"))
            {
                meleeCount++;
            }
            if (item.name.Contains("Tank"))
            {
                tankCount++;
            }
        }
    }
}
