using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttack : MonoBehaviour
{
    public GameObject target;
    public LayerMask player2;
    NavMeshAgent agent;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject projectilePosition;

    public float forward = 32f;
    public float upward = 12f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isOnNavMesh)
        {
            const int raycastCount = 30;
            const int raycastDistance = 10;

            for (int i = 0; i < raycastCount; i++)
            {
                Quaternion raycastRotation = Quaternion.AngleAxis((i / (float)raycastCount) * 180.0f - 90.0f, Vector3.up);
                Vector3 desiredDirection = transform.rotation * raycastRotation * Vector3.forward;
                desiredDirection.y = 0;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, desiredDirection, out hit, raycastDistance, player2))
                {
                    Debug.DrawRay(transform.position, desiredDirection * raycastDistance, Color.red);
                    agent.SetDestination(transform.position);
                    transform.LookAt(hit.point);
                    projectilePosition.transform.LookAt(hit.point);
                    AttackUnit();
                }
                else
                {
                    Debug.DrawRay(transform.position, desiredDirection * raycastDistance, Color.white);
                    agent.SetDestination(target.transform.position);
                }
            }
        }
    }

    private void AttackUnit()
    {
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, projectilePosition.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * forward, ForceMode.Impulse);
            rb.AddForce(transform.up * upward, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
