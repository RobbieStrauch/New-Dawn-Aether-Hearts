using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Reference: https://www.youtube.com/watch?v=dWrRb9pO8aQ&t=932s

public class EnemyStateCycle : StateMachine
{
    public EnemyStartState startState { get; private set; }
    public EnemyAttackState attackState { get; private set; }

    public LayerMask player2;
    public int attackDamage = 5;
    public float bulletAttackTime;
    public float swordAttackTime;
    public int attackRange = 10;
    public bool alreadyAttacked = false;
    public GameObject projectile;
    public GameObject projectilePosition;
    public float forward = 32f;
    public float upward = 1f;
    public float right = 1f;

    private void Awake()
    {
        startState = new EnemyStartState(this);
        attackState = new EnemyAttackState(this);
    }

    private void Start()
    {
        ChangeState(startState);
    }
}
