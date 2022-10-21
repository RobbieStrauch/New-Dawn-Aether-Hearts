using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraCommand : ICommand
{
    Vector3 position;
    Transform cam;
    GameObject Camera;

    public MoveCameraCommand(Vector3 position, Transform cam, GameObject Camera)
    {
        this.position = position;
        this.cam = cam;
        this.Camera = Camera;
    }

    public void Execute()
    {
        CameraMover.MoveCamera(cam);
    }

    public void Undo()
    {
        CameraMover.UndoMove(position, Camera);
    }
}
