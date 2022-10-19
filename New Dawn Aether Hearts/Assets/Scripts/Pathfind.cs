using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour
{
    private GameObject player;
    public List<GameObject> nodes;
    private List<GameObject> reverseNodes = new List<GameObject>();
    private List<GameObject> originalNodes = new List<GameObject>();

    ClickPath clickPath;
    Subject subject = new Subject();

    private float segmentTimer = 0;
    private float segmentTravelTime = 1.0f;
    private int segmentIndex = 1;

    private bool isMovingHere = false;
    private bool atStart = false;

    private int start;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            reverseNodes.Add(nodes[i]);
            originalNodes.Add(nodes[i]);
        }

        reverseNodes.Reverse();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z == nodes[nodes.Count - 1].transform.position.z)
        {
            start = nodes.Count - 1;
            atStart = true;
        }
        if (player.transform.position.z == nodes[0].transform.position.z)
        {
            start = 0;
            atStart = true;
        }
        if (MoveManager.instance.isMoving)
        {
            atStart = false;
        }

        if (isMovingHere)
        {
            Pathfinding();
        }
    }

    private void OnMouseDown()
    {
        if (!isMovingHere && !MoveManager.instance.isMoving && !PauseManager.instance.paused && atStart)
        {
            segmentIndex = 1;
            isMovingHere = true;
            MoveManager.instance.isMoving = true;

            clickPath = new ClickPath(player, new YellowMaterial());
            subject.AddObserver(clickPath);
            subject.Notify();
        }
    }

    public void Pathfinding()
    {
        if (start == 0)
        {
            StartPath();
        }
        if (start == nodes.Count - 1)
        {
            EndPath();
        }
    }

    public void StartPath()
    {
        segmentTimer += Time.deltaTime;

        if (segmentTimer > segmentTravelTime)
        {
            segmentTimer = 0f;
            segmentIndex += 1;

            if (segmentIndex >= originalNodes.Count)
            {
                segmentIndex = 0;
                isMovingHere = false;
                MoveManager.instance.isMoving = false;

                clickPath = new ClickPath(player, new GreenMaterial());
                subject.AddObserver(clickPath);
                subject.Notify();
            }
        }

        float t = segmentTimer / segmentTravelTime;

        Vector3 current, next;
        int currentIndex, nextIndex;

        nextIndex = segmentIndex;
        currentIndex = (nextIndex == 0) ? originalNodes.Count - 1 : nextIndex - 1;

        current = originalNodes[currentIndex].transform.position;
        next = originalNodes[nextIndex].transform.position;

        player.transform.position = LERP(current, next, t);
    }

    public void EndPath()
    {
        segmentTimer += Time.deltaTime;

        if (segmentTimer > segmentTravelTime)
        {
            segmentTimer = 0f;
            segmentIndex += 1;

            if (segmentIndex >= reverseNodes.Count)
            {
                segmentIndex = 0;
                isMovingHere = false;
                MoveManager.instance.isMoving = false;

                clickPath = new ClickPath(player, new GreenMaterial());
                subject.AddObserver(clickPath);
                subject.Notify();
            }
        }

        float t = segmentTimer / segmentTravelTime;

        Vector3 current, next;
        int currentIndex, nextIndex;

        nextIndex = segmentIndex;
        currentIndex = (nextIndex == 0) ? reverseNodes.Count - 1 : nextIndex - 1;

        current = reverseNodes[currentIndex].transform.position;
        next = reverseNodes[nextIndex].transform.position;

        player.transform.position = LERP(current, next, t);
    }

    public Vector3 LERP(Vector3 p0, Vector3 p1, float t)
    {
        return (1.0f - t) * p0 + t * p1;
    }
}
