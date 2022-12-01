using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : IState
{
    StateCycle stateCycle;
    NavMeshAgent agent;

    ClickPath clickPath;
    Subject subject;

    public MoveState(StateCycle stateCycle, Subject subject, ClickPath clickPath, NavMeshAgent agent)
    {
        this.stateCycle = stateCycle;
        this.subject = subject;
        this.clickPath = clickPath;
        this.agent = agent;
    }

    public void Enter()
    {
        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", false);
            stateCycle.GetComponent<Animator>().SetBool("isAttack", false);
        }
    }

    public void Tick()
    {

    }

    public void FixedTick()
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

        if (Vector3.Distance(stateCycle.gameObject.transform.position, stateCycle.targetPosition) <= 2)
        {
            stateCycle.ChangeState(stateCycle.startState);
        }
        else
        {
            agent.SetDestination(stateCycle.targetPosition);
        }
    }

    public void Exit()
    {

    }
}
