using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MoveState : IState
{
    StateCycle stateCycle;
    NavMeshAgent agent;
    LineRenderer lineRenderer;

    ClickPath clickPath;
    Subject subject;

    Vector3 newTargetPosition = Vector3.zero;
    Vector3 currentTargetPosition = Vector3.zero;

    public MoveState(StateCycle stateCycle, Subject subject, ClickPath clickPath, NavMeshAgent agent, LineRenderer lineRenderer)
    {
        this.stateCycle = stateCycle;
        this.subject = subject;
        this.clickPath = clickPath;
        this.agent = agent;
        this.lineRenderer = lineRenderer;

        currentTargetPosition = stateCycle.gameObject.transform.position;
    }

    public void Enter()
    {
        //Debug.Log("Move");

        lineRenderer.enabled = true;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;

        agent.SetDestination(currentTargetPosition);

        GameObject walkNoise = stateCycle.transform.Find("Walk Noise").gameObject;
        walkNoise.GetComponent<AudioSource>().Play();

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

        if (stateCycle.isSelected)
        {
            if (UnitSelection.instance.unitSelectedList.Count > 1)
            {
                newTargetPosition = GetNewPosition(stateCycle.targetPosition, stateCycle.radius, UnitSelection.instance.unitSelectedList.Count);
                agent.SetDestination(newTargetPosition);
                currentTargetPosition = newTargetPosition;
            }
            if (UnitSelection.instance.unitSelectedList.Count == 1)
            {
                agent.SetDestination(stateCycle.targetPosition);
                currentTargetPosition = stateCycle.targetPosition;
            }
        }

        try
        {
            float x = stateCycle.transform.position.x;
            float y = stateCycle.transform.position.y;
            float z = stateCycle.transform.position.z;

            float rx = stateCycle.transform.eulerAngles.x;
            float ry = stateCycle.transform.eulerAngles.y;
            float rz = stateCycle.transform.eulerAngles.z;

            //Debug.Log(stateCycle.transform.localRotation.y + " or " + stateCycle.transform.rotation.y + " or " + stateCycle.transform.eulerAngles.y);

            byte[] buffer = Encoding.ASCII.GetBytes(UnitClient.instance.GetClient().Client.LocalEndPoint + "|" + stateCycle.name + "|" + x.ToString() + "|" + y.ToString() + "|" + z.ToString() + "|" + rx.ToString() + "|" + ry.ToString() + "|" + rz.ToString());
            UnitClient.instance.GetClient().Send(buffer, buffer.Length);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

        //Debug.Log(Vector3.Distance(stateCycle.transform.position, currentTargetPosition) + "<" + agent.stoppingDistance);

        if (Vector3.Distance(stateCycle.transform.position, currentTargetPosition) < agent.stoppingDistance)
        {
            agent.SetDestination(stateCycle.transform.position);
            stateCycle.ChangeState(stateCycle.startState);
        }
        //else
        //{
        //    agent.SetDestination(currentTargetPosition);
        //}

        //if (Vector3.Distance(stateCycle.gameObject.transform.position, currentTargetPosition) <= (stateCycle.radius / 2f))
        //{
        //    stateCycle.ChangeState(stateCycle.startState);
        //}
    }

    public void Exit()
    {
        lineRenderer.enabled = false;

        GameObject walkNoise = stateCycle.transform.Find("Walk Noise").gameObject;
        walkNoise.GetComponent<AudioSource>().Stop();
    }

    // Reference: https://www.youtube.com/watch?v=mCIkCXz9mxI&t=883s
    private Vector3 GetNewPosition(Vector3 startPosition, float distance, int unitCount)
    {
        float angle = stateCycle.priority * (360.0f / unitCount);
        Vector3 direction = ApplyRotationToVector(new Vector3(1.0f, 0.0f), angle);
        Vector3 position = startPosition + direction * distance;

        return position;
    }

    // Reference: https://www.youtube.com/watch?v=mCIkCXz9mxI&t=883s
    private Vector3 ApplyRotationToVector(Vector3 vector, float angle)
    {
        return Quaternion.Euler(0.0f, angle, 0.0f) * vector;
    }
}
