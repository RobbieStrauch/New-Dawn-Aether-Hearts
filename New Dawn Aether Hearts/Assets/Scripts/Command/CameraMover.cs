using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public static CameraMover instance;
    static List<Transform> Movements;

    public static void MoveCamera(Transform Position)
    {
        Transform newposition = Position;

        if (Movements == null)
        {
            Movements = new List<Transform>();
        }

        Movements.Add(newposition);
    }

    public static void UndoMove(Vector3 position, GameObject Cam)
    {
        for (int i = 0; i < Movements.Count; i++)
        {
            if (Movements[i].position == position)
            {
                Cam.transform.position = Movements[i].position;
                Movements.RemoveAt(i);
                break;
            }
        }
    }
}
