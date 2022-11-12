using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonExpand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    private bool isEntered = false;
    private Vector3 normal, expand;
    private Vector3 LERP(Vector3 p0, Vector3 p1, float t) { return (1.0f - t) * p0 + t * p1; }

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void FixedUpdate()
    {
        if (isEntered)
        {
            button.transform.localScale = LERP(button.transform.localScale, expand, Time.deltaTime);
            Invoke("Reset", 1.0f);
        }
    }

    private void Reset()
    {
        isEntered = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEntered)
        {
            normal = button.transform.localScale;
            expand = normal * 1.25f;
            isEntered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.transform.localScale = normal;
        isEntered = false;
    }
}