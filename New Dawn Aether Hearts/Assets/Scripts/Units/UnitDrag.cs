using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Reference from: https://www.youtube.com/watch?v=vAVi04mzeKk&t=2s

public class UnitDrag : MonoBehaviour
{
    Camera mainCamera;

    public Image dragboxImage;

    Rect dragbox;

    Vector2 startClick = Vector2.zero;

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
                dragbox = new Rect();
                startClick = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                dragboxImage.rectTransform.anchoredPosition = startClick;
            }

            if (Input.GetMouseButton(1))
            {
                CreateDragbox();
            }

            if (Input.GetMouseButtonUp(1))
            {
                SelectUnits();
            }
        }
    }

    void CreateDragbox()
    {
        dragboxImage.gameObject.SetActive(true);
        Vector2 endPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 startPosition = startClick;

        if (endPosition.x < startPosition.x)
        {
            dragbox.xMin = endPosition.x;
            dragbox.xMax = startPosition.x;
        }
        else
        {
            dragbox.xMin = startPosition.x;
            dragbox.xMax = endPosition.x;
        }

        if (endPosition.y < startPosition.y)
        {
            dragbox.yMin = endPosition.y;
            dragbox.yMax = startPosition.y;
        }
        else
        {
            dragbox.yMin = startPosition.y;
            dragbox.yMax = endPosition.y;
        }

        dragboxImage.rectTransform.offsetMin = dragbox.min;
        dragboxImage.rectTransform.offsetMax = dragbox.max;
    }

    void SelectUnits()
    {
        dragboxImage.gameObject.SetActive(false);
        foreach (var unit in UnitSelection.instance.unitList)
        {
            if (dragbox.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelection.instance.DragSelect(unit);
            }
        }
    }
}
