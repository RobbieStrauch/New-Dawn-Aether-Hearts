using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

/*
 Refrence
https://www.youtube.com/watch?v=tGAaj824i4s&t=130s
Used to Learn how to move the camera based on mouse position
 */

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float movespeed;
    public float thickness;

    //PlayerInput InputAction;

    public bool freeze = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            freeze = !freeze;
        }


        Vector3 Pos = transform.position;

        if (freeze == false)
        {
            //up
            if (Input.mousePosition.y >= Screen.height - thickness)
            {
                Pos.z += movespeed * Time.deltaTime;
            }

            //down
            if (Input.mousePosition.y <= thickness)
            {
                Pos.z -= movespeed * Time.deltaTime;
            }

            //right
            if (Input.mousePosition.x >= Screen.height - thickness)
            {
                Pos.x += movespeed * Time.deltaTime;
            }

            //left
            if (Input.mousePosition.x <= thickness)
            {
                Pos.x -= movespeed * Time.deltaTime;
            }

            transform.position = Pos;
        }

    }
}