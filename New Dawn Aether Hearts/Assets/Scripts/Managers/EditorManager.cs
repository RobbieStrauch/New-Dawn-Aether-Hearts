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
    public GameObject groundObject;
    public Material transparentMaterial;
    public Material normalMaterial;

    public bool instantiated = false;
    public GameObject item;

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
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        groundObject.layer = 13;

        if (instantiated && Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            item.GetComponent<Renderer>().material = normalMaterial;
            item.GetComponent<Collider>().enabled = true;
            item.GetComponent<NavMeshAgent>().enabled = true;
            instantiated = false;
        }
    }

    void Update()
    {
        if (instantiated)
        {
            item.GetComponent<Renderer>().material = transparentMaterial;
            groundObject.layer = 0;

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                item.transform.position = hit.point + Vector3.up * 2;
            }
        }
    }
}
