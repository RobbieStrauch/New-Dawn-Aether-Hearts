using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference from: https://www.youtube.com/watch?v=vAVi04mzeKk&t=2s

public class UnitClick : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject target;

    public LayerMask selectable;
    public LayerMask ground;

    public event System.Action<Vector3> NewTargetAcquired = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EditorManager.instance.instantiated)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectable))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        UnitSelection.instance.ContinueClickSelect(hit.collider.gameObject);
                    }
                    else
                    {
                        UnitSelection.instance.ClickSelect(hit.collider.gameObject);
                    }
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        UnitSelection.instance.DeselectAll();
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    target.transform.position = hit.point;
                    target.SetActive(true);
                    NewTargetAcquired.Invoke(hit.point);
                    Fade.instance.StartFading();
                }
            }
        }
    }
}
