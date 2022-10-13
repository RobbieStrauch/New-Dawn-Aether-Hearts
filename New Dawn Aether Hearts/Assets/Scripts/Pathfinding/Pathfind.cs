using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour
{
    private GameObject player;
    public List<GameObject> nodes;
    private List<GameObject> reverseNodes = new List<GameObject>();
    private List<GameObject> originalNodes = new List<GameObject>();

    private float _segmentTimer = 0;
    private float _segmentTravelTime = 1.0f;
    private int _segmentIndex = 1;

    private bool isMovingHere = false;

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
        if (isMovingHere)
        {
            Pathfinding();
        }

        if (player.transform.position.z == nodes[nodes.Count - 1].transform.position.z)
        {
            start = nodes.Count - 1;
        }
        if (player.transform.position.z == nodes[0].transform.position.z)
        {
            start = 0;
        }
    }

    private void OnMouseDown()
    {
        if (!isMovingHere && !MoveManager.instance.isMoving)
        {
            _segmentIndex = 1;
            isMovingHere = true;
            MoveManager.instance.isMoving = true;
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
        _segmentTimer += Time.deltaTime;

        if (_segmentTimer > _segmentTravelTime)
        {
            _segmentTimer = 0f;
            _segmentIndex += 1;

            if (_segmentIndex >= originalNodes.Count)
            {
                _segmentIndex = 0;
                isMovingHere = false;
                MoveManager.instance.isMoving = false;
            }
        }

        float t = _segmentTimer / _segmentTravelTime;

        Vector3 p0, p1;
        int p0_index, p1_index;

        p1_index = _segmentIndex;
        p0_index = (p1_index == 0) ? originalNodes.Count - 1 : p1_index - 1;

        p0 = originalNodes[p0_index].transform.position;
        p1 = originalNodes[p1_index].transform.position;

        player.transform.position = LERP(p0, p1, t);
    }

    public void EndPath()
    {
        _segmentTimer += Time.deltaTime;

        if (_segmentTimer > _segmentTravelTime)
        {
            _segmentTimer = 0f;
            _segmentIndex += 1;

            if (_segmentIndex >= reverseNodes.Count)
            {
                _segmentIndex = 0;
                isMovingHere = false;
                MoveManager.instance.isMoving = false;
            }
        }

        float t = _segmentTimer / _segmentTravelTime;

        Vector3 p0, p1;
        int p0_index, p1_index;

        p1_index = _segmentIndex;
        p0_index = (p1_index == 0) ? reverseNodes.Count - 1 : p1_index - 1;

        p0 = reverseNodes[p0_index].transform.position;
        p1 = reverseNodes[p1_index].transform.position;

        player.transform.position = LERP(p0, p1, t);
    }

    public Vector3 LERP(Vector3 p0, Vector3 p1, float t)
    {
        return (1.0f - t) * p0 + t * p1;
    }
}
