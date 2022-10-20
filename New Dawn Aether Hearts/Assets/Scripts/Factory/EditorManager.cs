using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public PlayerActions inputAction;

    public Camera mainCam;
    public Camera editorCam;

    public bool editorMode = false;

    Vector3 mousePos;
    public GameObject prefab1;
    public GameObject prefab2;
    public GameObject item;
    public bool instantiated = false;

    //Will send notifications that something has happened to whoever is interested
    Subject subject = new Subject();

    // Command
    ICommand command;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Editor1.EditorMode.performed += cntxt => EnterEditorMode();

        inputAction.Editor1.AddItem1.performed += cntxt => AddItem(1);
        inputAction.Editor1.AddItem2.performed += cntxt => AddItem(2);
        inputAction.Editor1.DropItem.performed += cntxt => DropItem();

        mainCam.enabled = true;
        editorCam.enabled = false;

    }

    public void EnterEditorMode()
    {
        mainCam.enabled = !mainCam.enabled;
        editorCam.enabled = !editorCam.enabled;
    }

    public void AddItem(int itemId)
    {
        if (editorMode && !instantiated)
        {
            switch (itemId)
            {
                case 1:
                    item = Instantiate(prefab1);
                    break;
                case 2:
                    item = Instantiate(prefab2);
                    break;
                default:
                    break;
            }
            subject.Notify();
            instantiated = true;
        }
    }

    public void DropItem()
    {
        if (editorMode && instantiated)
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Collider>().enabled = true;

            // Add item transform to items list
            command = new PlaceItemCommand(item.transform.position, item.transform);
            CommandInvoker.AddCommand(command);

            instantiated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if we are in editor mode
        if (mainCam.enabled == false && editorCam.enabled == true)
        {
            editorMode = true;

        }
        else
        {
            editorMode = false;

        }

        if (instantiated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 40f);

            item.transform.position = editorCam.ScreenToWorldPoint(mousePos);
        }

    }
}
