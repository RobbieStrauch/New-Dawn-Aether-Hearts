using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EnemyStartState : IState
{
    EnemyStateCycle stateCycle;

    public EnemyStartState(EnemyStateCycle stateCycle)
    {
        this.stateCycle = stateCycle;
    }

    public void Enter()
    {
        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", true);
        }
    }

    public void Tick()
    {
        const int raycastCount = 100;

        for (int i = 0; i < raycastCount; i++)
        {
            Quaternion raycastRotation = Quaternion.AngleAxis((i / (float)raycastCount) * 180.0f - 90.0f, Vector3.up);
            Vector3 desiredDirection = stateCycle.transform.rotation * raycastRotation * Vector3.forward;
            desiredDirection.y = 0;

            RaycastHit hit;
            if (Physics.Raycast(stateCycle.transform.position, desiredDirection, out hit, stateCycle.attackRange, stateCycle.player2))
            {
                stateCycle.ChangeState(stateCycle.attackState);
            }
            else
            {
                Debug.DrawRay(stateCycle.transform.position, desiredDirection * stateCycle.attackRange, Color.white);
            }
        }
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", false);
        }
    }
}
