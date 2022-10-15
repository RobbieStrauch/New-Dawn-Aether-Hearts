using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraCommand : ICommand
{
    Vector3 position;
    Transform cam;

    public MoveCameraCommand(Vector3 position, Transform cam)
    {
        this.position = position;
        this.cam = cam;
    }

    public void Execute()
    {
        CameraMover.MoveCamera(cam);
    }

    public void Undo()
    {
        CameraMover.UndoMove(position);
    }
}
