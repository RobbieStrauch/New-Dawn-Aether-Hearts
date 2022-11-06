using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitPrototype : MonoBehaviour
{
    public List<UnitData> allData;
    public GameObject buttonPanel;
    public GameObject buttonPrefab;

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

        for (int i = 0; i < allData.Count; i++)
        {
            var button = Instantiate(buttonPrefab);
            button.transform.SetParent(buttonPanel.transform);
            button.gameObject.name = allData[i]._name + " Button";
            button.GetComponentInChildren<TextMeshProUGUI>().text = allData[i]._name;
            button.GetComponent<Button>().onClick.AddListener(delegate { Spawner(button); });
        }
    }

    void Spawner(GameObject button)
    {
        var b = button.GetComponentInChildren<TextMeshProUGUI>();

        switch (b.text)
        {
            case "Scout":
                EditorManager.instance.item = scout.Clone().Spawn();
                break;
            case "Ranged":
                EditorManager.instance.item = ranged.Clone().Spawn();
                break;
            case "Melee":
                EditorManager.instance.item = melee.Clone().Spawn();
                break;
            case "Tank":
                EditorManager.instance.item = tank.Clone().Spawn();
                break;
            default:
                break;
        }

        EditorManager.instance.instantiated = true;
    }
}
