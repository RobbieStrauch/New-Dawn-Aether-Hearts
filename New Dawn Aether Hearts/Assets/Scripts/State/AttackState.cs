using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    StateCycle stateCycle;
    NavMeshAgent agent;

    ClickPath clickPath;
    Subject subject;

    float elapsedTime = 0f;
    bool timerActive = false;

    GameObject saveObject;

    public AttackState(StateCycle stateCycle, Subject subject, ClickPath clickPath, NavMeshAgent agent)
    {
        this.stateCycle = stateCycle;
        this.subject = subject;
        this.clickPath = clickPath;
        this.agent = agent;
    }

    public void Enter()
    {
        clickPath = new ClickPath(stateCycle.gameObject, new YellowMaterial());
        subject.AddObserver(clickPath);
        subject.Notify();

        StartTimer();
    }

    public void Tick()
    {
        if (timerActive)
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime > stateCycle.timeBetweenAttacks)
        {
            ResetAttack();
            elapsedTime = 0f;
        }

        const int raycastCount = 30;
        const int raycastDistance = 10;

        for (int i = 0; i < raycastCount; i++)
        {
            Quaternion raycastRotation = Quaternion.AngleAxis((i / (float)raycastCount) * 180.0f - 90.0f, Vector3.up);
            Vector3 desiredDirection = stateCycle.transform.rotation * raycastRotation * Vector3.forward;
            desiredDirection.y = 0;

            RaycastHit hit;
            if (Physics.Raycast(stateCycle.transform.position, desiredDirection, out hit, raycastDistance, stateCycle.player2))
            {
                saveObject = hit.collider.gameObject;

                Debug.DrawRay(stateCycle.transform.position, desiredDirection * raycastDistance, Color.red);
                agent.SetDestination(stateCycle.transform.position);
                stateCycle.transform.LookAt(hit.point);
                stateCycle.projectilePosition.transform.LookAt(hit.point);
                AttackUnit();
            }

            if (saveObject == null)
            {
                stateCycle.ChangeState(stateCycle.moveState);
            }
        }
    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        StopTimer();
    }

    private void AttackUnit()
    {
        if (!stateCycle.alreadyAttacked)
        {
            //Rigidbody rb = MonoBehaviour.Instantiate(stateCycle.projectile, stateCycle.projectilePosition.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            Rigidbody rb = ObjectPooler.instance.SpawnFromPool("Bullet", stateCycle.projectilePosition.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(stateCycle.transform.forward * stateCycle.forward, ForceMode.Impulse);
            rb.AddForce(stateCycle.transform.up * stateCycle.upward, ForceMode.Impulse);

            stateCycle.alreadyAttacked = true;
        }
    }
    private void ResetAttack()
    {
        stateCycle.alreadyAttacked = false;
    }

    public void StartTimer()
    {
        timerActive = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
