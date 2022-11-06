using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    PlayerActions playerAction;

    Camera mainCamera;
    public LayerMask ground;
    public Material transparentMaterial;
    public Material normalMaterial;

    public bool instantiated = false;
    public GameObject item;

    Subject subject = new Subject();
    ICommand command;

    void Start()
    {
        mainCamera = Camera.main;

        if (instance == null)
        {
            instance = this;
        }

        playerAction = InputController.controller.InputAction;
        playerAction.Editor.DropItem.performed += cntxt => DropItem();
    }

    private void DropItem()
    {
        if (instantiated)
        {
            item.GetComponent<Renderer>().material = normalMaterial;
            item.GetComponent<Collider>().enabled = true;
            item.GetComponent<NavMeshAgent>().enabled = true;

            //command = new PlaceItemCommand(item.transform.position, item.transform);
            //CommandInvoker.AddCommand(command);

            instantiated = false;
        }
    }

    void Update()
    {
        if (instantiated)
        {
            item.GetComponent<Renderer>().material = transparentMaterial;

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                item.transform.position = hit.point + Vector3.up * 2;
            }
        }
    }
}
