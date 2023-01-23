using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

// Reference: https://www.youtube.com/watch?v=dTlSk9cP7TA&t=587s

public class GizmoLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    NavMeshAgent agent;
    List<Vector3> gizmoLines;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawPath();
    }

    void DrawPath()
    {
        if (agent.path.corners.Length < 2)
        {
            return;
        }

        int i = 1;
        while (i < agent.path.corners.Length)
        {
            lineRenderer.positionCount = agent.path.corners.Length;
            gizmoLines = agent.path.corners.ToList();
            for (int j = 0; j < gizmoLines.Count; j++)
            {
                lineRenderer.SetPosition(j, gizmoLines[j]);
            }
            i++;
        }
    }
}
