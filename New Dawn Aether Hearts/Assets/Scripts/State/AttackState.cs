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
        //Debug.Log(stateCycle.gameObject.name + " Attack State");

        //clickPath = new ClickPath(stateCycle.gameObject, new YellowMaterial());
        //subject.AddObserver(clickPath);
        //subject.Notify();

        StartTimer();
    }

    public void Tick()
    {
        if (timerActive)
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime > stateCycle.bulletAttackTime && stateCycle.gameObject.GetComponent<Unit>().unitType != Unit.UnitType.Melee)
        {
            ResetBulletAttack();
            elapsedTime = 0f;
        }

        if (elapsedTime > stateCycle.swordAttackTime && stateCycle.gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Melee)
        {
            ResetSwordAttack();
            elapsedTime = 0f;
        }

        const int raycastCount = 30;
        //const int raycastDistance = 10;

        for (int i = 0; i < raycastCount; i++)
        {
            Quaternion raycastRotation = Quaternion.AngleAxis((i / (float)raycastCount) * 180.0f - 90.0f, Vector3.up);
            Vector3 desiredDirection = stateCycle.transform.rotation * raycastRotation * Vector3.forward;
            desiredDirection.y = 0;

            RaycastHit hit;
            if (Physics.Raycast(stateCycle.transform.position, desiredDirection, out hit, stateCycle.attackRange, stateCycle.player2))
            {
                saveObject = hit.collider.gameObject;

                Debug.DrawRay(stateCycle.transform.position, desiredDirection * stateCycle.attackRange, Color.red);
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
        if (stateCycle.gameObject.GetComponent<Unit>().unitType == Unit.UnitType.Melee)
        {
            if (!stateCycle.alreadyAttacked)
            {
                //stateCycle.gameObject.transform.GetChild(1).rotation = new Quaternion(90f, 0f, 0f, 1f);
                //stateCycle.gameObject.transform.GetChild(1).position += new Vector3(0.0f, -0.5f, 0.8f);

                stateCycle.alreadyAttacked = true;
            }
        }
        else
        {
            if (!stateCycle.alreadyAttacked)
            {
                //Rigidbody rb = MonoBehaviour.Instantiate(stateCycle.projectile, stateCycle.projectilePosition.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                //Debug.Log(stateCycle.transform.rotation);
                //Debug.Log(stateCycle.projectilePosition.transform.rotation);
                //Debug.Log(Quaternion.identity + "\n");
                Rigidbody rb = ObjectPooler.instance.SpawnFromPool("Bullet", stateCycle.projectilePosition.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(stateCycle.transform.forward * stateCycle.forward, ForceMode.Impulse);
                rb.AddForce(stateCycle.transform.up * stateCycle.upward, ForceMode.Impulse);

                stateCycle.alreadyAttacked = true;
            }
        }
    }

    private void ResetBulletAttack()
    {
        stateCycle.alreadyAttacked = false;
    }

    private void ResetSwordAttack()
    {
        //stateCycle.gameObject.transform.GetChild(1).rotation = new Quaternion(0f, 0f, 0f, 1f);
        //stateCycle.gameObject.transform.GetChild(1).position += new Vector3(0.0f, 0.5f, -0.8f);
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
