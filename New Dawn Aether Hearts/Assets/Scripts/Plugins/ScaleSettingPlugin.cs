using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;

public class ScaleSettingPlugin : MonoBehaviour
{

    [DllImport("ScalePluggin")]
    private static extern int GetID();

    [DllImport("ScalePluggin")]
    private static extern void SetID(int id);

    [DllImport("ScalePluggin")]
    private static extern Vector3 GetScale();

    [DllImport("ScalePluggin")]
    private static extern void SetScale(float x, float y, float z);

    PlayerActions InputAction;

    // Start is called before the first frame update
    void Start()
    {
        InputAction = InputController.controller.InputAction;

        InputAction.Scale.Increase.performed += cntxt => ScaleUp();
        InputAction.Scale.Decrease.performed += cntxt => ScaleDown();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ScaleUp()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            Vector3 pos = GetScale();
            SetScale(pos.x + 1, pos.y + 1, pos.z + 1);
        }
    }

    void ScaleDown()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            Vector3 pos = GetScale();
            SetScale(pos.x - 1, pos.y - 1, pos.z - 1);
        }
    }
}
