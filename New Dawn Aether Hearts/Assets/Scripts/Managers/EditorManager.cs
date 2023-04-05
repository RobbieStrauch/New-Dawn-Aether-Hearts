using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Text;

public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    public Camera mainCamera;
    public LayerMask ground;
    public GameObject groundObject;
    public Material transparentMaterial;
    public Material normalMaterial;
    public bool instantiated = false;
    public GameObject item;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Drop()
    {
        try
        {
            float x = item.transform.position.x;
            float y = item.transform.position.y;
            float z = item.transform.position.z;

            float rx = item.transform.eulerAngles.x;
            float ry = item.transform.eulerAngles.y;
            float rz = item.transform.eulerAngles.z;

            byte[] buffer = Encoding.ASCII.GetBytes(UnitClient.instance.GetClient().Client.LocalEndPoint + "|" + item.name + "|Activate" + "|" + x.ToString() + "|" + y.ToString() + "|" + z.ToString() + "|" + rx.ToString() + "|" + ry.ToString() + "|" + rz.ToString());
            UnitClient.instance.GetClient().Send(buffer, buffer.Length);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

        item.GetComponent<Collider>().enabled = true;
        item.GetComponent<NavMeshAgent>().enabled = true;
        instantiated = false;
    }

    private void DropItem()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 

        groundObject.layer = 13;

        if (instantiated && Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Drop();
            }
        }
    }

    void Update()
    {
        if (instantiated)
        {
            groundObject.layer = 0;

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                item.transform.position = hit.point + Vector3.up * 2;
            }
        }

        DropItem();
    }
}
