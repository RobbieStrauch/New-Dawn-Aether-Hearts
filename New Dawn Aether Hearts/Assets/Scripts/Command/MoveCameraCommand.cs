using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraCommand : ICommand
{
    Vector3 position;
    Transform cam;
    GameObject camera;

    public MoveCameraCommand(Vector3 position, Transform cam, GameObject camera)
    {
        this.position = position;
        this.cam = cam;
        this.camera = camera;
    }

    public void Execute()
    {
        CameraMover.MoveCamera(cam);
    }

    public void Undo()
    {
        CameraMover.UndoMove(position, camera);
    }
}
