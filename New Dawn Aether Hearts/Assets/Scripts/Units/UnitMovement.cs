using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Sound sound;

    Camera myCamera;
    NavMeshAgent agent;
    
    public LayerMask ground;
    private GameObject target;

    ClickPath clickPath;
    Subject subject = new Subject();

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Target");
        myCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EditorManager.instance.instantiated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    agent.SetDestination(hit.point);

                    clickPath = new ClickPath(target, new YellowMaterial());
                    subject.AddObserver(clickPath);
                    subject.Notify();
                    sound.noise();
                }
            }
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= 2)
        {
            agent.isStopped = true;
            sound.stop();
        }
        else
        {
            agent.isStopped = false;
        }
    }
}
