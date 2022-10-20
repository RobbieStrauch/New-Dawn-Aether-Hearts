using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitButton : MonoBehaviour
{
    private UnitFactory factory;

    TextMeshProUGUI btnText;
    // Start is called before the first frame update
    void Start()
    {
        factory = GameObject.Find("Game Manager").GetComponent<UnitFactory>();

        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnClickSpawn()
    {
        switch (btnText.text)
        {

           
        }
    }
}
