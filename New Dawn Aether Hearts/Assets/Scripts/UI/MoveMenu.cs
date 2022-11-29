using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMenu : MonoBehaviour
{
    public Transform currentMenu, nextMenu, currentPosition, nextPosition;

    private Vector3 current, next;
    private bool isPressed = false;
    private Vector3 LERP(Vector3 p0, Vector3 p1, float t) { return (1.0f - t) * p0 + t * p1; }

    private void FixedUpdate()
    {
        if (isPressed)
        {
            currentMenu.transform.position = LERP(currentMenu.transform.position, next, Time.deltaTime * 5);
            nextMenu.transform.position = LERP(nextMenu.transform.position, current, Time.deltaTime * 5);

            Invoke("Reset", 1.0f);
        }
    }

    private void Reset()
    {
        isPressed = false;
    }

    public void Press()
    {
        if (!isPressed)
        {
            current = currentPosition.transform.position;
            next = nextPosition.transform.position;

            isPressed = true;
        }
    }
}