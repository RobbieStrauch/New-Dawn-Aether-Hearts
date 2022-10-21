using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitButton : MonoBehaviour
{
    private UnitFactory factory;
    private EditorManager editor;
    TextMeshProUGUI btnText;
    // Start is called before the first frame update
    void Start()
    {
        factory = GameObject.Find("Game Manager").GetComponent<UnitFactory>();
        editor = EditorManager.instance;
        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClickSpawn()
    {
        switch (btnText.text)
        {
            case "scout":
                editor.item = factory.Get_Factory("scout").Create(factory.prefab1);
                break;
            case "hero":
                editor.item = factory.Get_Factory("hero").Create(factory.prefab2);
                break;
            default:
                break;
        }

        editor.instantiated = true;
    }
}
