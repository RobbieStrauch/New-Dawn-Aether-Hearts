using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //public GameObject target;
    public LayerMask player2;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject projectilePosition;

    public float forward = 32f;
    public float upward = 12f;
    public float right = 1f;
    public float attackRange = 10f;
    public int attackDamage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        const int raycastCount = 100;

        for (int i = 0; i < raycastCount; i++)
        {
            Quaternion raycastRotation = Quaternion.AngleAxis((i / (float)raycastCount) * 180.0f - 90.0f, Vector3.up);
            Vector3 desiredDirection = transform.rotation * raycastRotation * Vector3.forward;
            desiredDirection.y = 0;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, desiredDirection, out hit, attackRange, player2))
            {
                if (gameObject.GetComponent<Animator>())
                {
                    gameObject.GetComponent<Animator>().SetBool("isAttack", true);
                }

                Debug.DrawRay(transform.position, desiredDirection * attackRange, Color.red);
                Vector3 temp = hit.collider.gameObject.transform.GetChild(1).gameObject.transform.position;
                var lookPosition = temp - transform.position;
                lookPosition.y = 0;
                var rotate = Quaternion.LookRotation(lookPosition);
                transform.rotation = rotate;

                AttackUnit();
            }
            else
            {
                if (gameObject.GetComponent<Animator>())
                {
                    gameObject.GetComponent<Animator>().SetBool("isStart", true);
                }
                Debug.DrawRay(transform.position, desiredDirection * attackRange, Color.white);
            }
        }
    }

    private void AttackUnit()
    {
        if (gameObject.GetComponent<EnemyUnit>().enemyUnitType == EnemyUnit.EnemyUnitType.Melee)
        {
            if (!alreadyAttacked)
            {
                if (gameObject.GetComponent<Animator>())
                {
                    gameObject.GetComponent<Animator>().Play("Attack", 0, 0f);
                }
                if (gameObject.transform.GetComponentInChildren<Animation>())
                {
                    gameObject.transform.GetComponentInChildren<Animation>().Play();
                }
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        else
        {
            if (!alreadyAttacked)
            {
                if (gameObject.GetComponent<Animator>())
                {
                    gameObject.GetComponent<Animator>().Play("Attack", 0, 0f);
                }

                GameObject bullet = MonoBehaviour.Instantiate(projectile, projectilePosition.transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().ChangeDamage(attackDamage);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();

                //Rigidbody flashRB = ObjectPooler.instance.SpawnFromPool("Flash", projectilePosition.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

                rb.AddForce(gameObject.transform.forward * forward, ForceMode.Impulse);
                rb.AddForce(gameObject.transform.up * upward, ForceMode.Impulse);
                if (gameObject.GetComponent<Animator>())
                {
                    rb.AddForce(gameObject.transform.right * (gameObject.transform.rotation.y + right), ForceMode.Impulse);
                }

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}