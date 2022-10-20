using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerActions InputAction;

    public static InputController controller;

    public void OnEnable()
    {
        InputAction.Enable();
    }

    public void Ondisable()
    {
        InputAction.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }

        InputAction = new PlayerActions();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
