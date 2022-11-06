using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 Refrence
https://www.youtube.com/watch?v=tGAaj824i4s&t=130s
Used to Learn how to move the camera based on mouse position
 */

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private Vector3 Pos;
    private Vector3[] Past;
    int a = 0;

    public float movespeed;
    public float thickness;
 

    PlayerActions InputAction;


    private void OnEnable()
    {
        InputAction.Camera.Enable();
    }

    private void OnDisable()
    {
        InputAction.Camera.Disable();
    }

    public bool freeze = true;

    private void Start()
    {
        Past = new Vector3[10];
    }

    void Awake()
    {
        InputAction = new PlayerActions();


        InputAction.Camera.Stop.performed += cntxt => Stop();
        InputAction.Camera.PrimaryTarget.performed += cntxt => Move(1);
        InputAction.Camera.SecondaryTarget.performed += cntxt => Move(2);
        InputAction.Camera.ThirdTarget.performed += cntxt => Move(3);
        InputAction.Camera.FourthTarget.performed += cntxt => Move(4);
        InputAction.Camera.FifthTarget.performed += cntxt => Move(5);
        InputAction.Camera.Undo.performed += cntxt => Undo();
    }

    void Update()
    {

        Pos = transform.position;

       
        if(a == 9)
        {
            Past[1] = Past[2];
            Past[2] = Past[3];
            Past[3] = Past[4];
            Past[4] = Past[5];
            Past[5] = Past[6];
            Past[6] = Past[7];
            Past[7] = Past[8];
            Past[8] = Past[9];
            a--;
        }
       

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

    private void Stop()
    {
        freeze = !freeze;
    }

    private void Move(int value)
    {
        Pos = transform.position;

        switch (value)
        {
            case 1:
                Pos.Set(-2, 41, 60);
                a++;
                Past[a] = Pos;

                break;

            case 2:
                Pos.Set(-110, 41, 60);
                a++;
                Past[a] = Pos;              

                break;

            case 3:
                Pos.Set(105, 41, 60);
                a++;
                Past[a] = Pos;
                

                break;

            case 4:
                Pos.Set(-54, 41, 11);
                a++;
                Past[a] = Pos;


                break;

            case 5:
                Pos.Set(47, 41, 105);
                a++;
                Past[a] = Pos;


                break;
        }

        transform.position = Pos;
    }

    private void Undo()
    {
        if(a >= 2)
        {
            a--;
            Pos = transform.position;

            Pos.Set(Past[a].x, Past[a].y, Past[a].z);

            transform.position = Pos;
        }

    }
}