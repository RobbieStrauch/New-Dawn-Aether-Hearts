using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineMove : MonoBehaviour
{
    public float speed = 3f;
    public float smoothTime = 0.5f;
    public Vector3 target = new Vector3(-31, 1, 78);
    Vector3 currentVel;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVel, smoothTime);
    }
}
