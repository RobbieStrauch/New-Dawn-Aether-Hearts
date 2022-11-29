using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitPrototype : MonoBehaviour
{
    public List<UnitData> allData;
    public List<GameObject> unitPanels;

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

        if (button.gameObject.transform.parent.name == "ScoutPanel" && resource.Agold >= scout.Cost())
        {
            resource.Agold -= scout.Cost();
            EditorManager.instance.item = scout.Clone().Spawn();
            EditorManager.instance.instantiated = true;
        }
        if (button.gameObject.transform.parent.name == "RangedPanel" && resource.Agold >= ranged.Cost())
        {
            resource.Agold -= ranged.Cost();
            EditorManager.instance.item = ranged.Clone().Spawn();
            EditorManager.instance.instantiated = true;
        }
        if (button.gameObject.transform.parent.name == "MeleePanel" && resource.Agold >= melee.Cost())
        {
            resource.Agold -= melee.Cost();
            EditorManager.instance.item = melee.Clone().Spawn();
            EditorManager.instance.instantiated = true;
        }
        if (button.gameObject.transform.parent.name == "TankPanel" && resource.Agold >= tank.Cost())
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
}
