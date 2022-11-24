using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Reference: https://www.youtube.com/watch?v=dWrRb9pO8aQ&t=932s

public class StateCycle : StateMachine
{
    public StartState startState { get; private set; }
    public MoveState moveState { get; private set; }
    public AttackState attackState { get; private set; }

    UnitClick unitClick;
    NavMeshAgent agent;

    public Vector3 targetPosition { get; set; }

    public LayerMask player2;
    public float bulletAttackTime;
    public float swordAttackTime;
    public int attackRange = 10;
    public bool alreadyAttacked = false;
    public GameObject projectile;
    public GameObject projectilePosition;
    public float forward = 32f;
    public float upward = 1f;

    ClickPath clickPath;
    Subject subject = new Subject();

    private void Awake()
    {
        unitClick = GameObject.Find("Game Manager").GetComponent<UnitClick>();
        agent = GetComponent<NavMeshAgent>();

        startState = new StartState(this, subject, clickPath, unitClick);
        moveState = new MoveState(this, subject, clickPath, agent);
        attackState = new AttackState(this, subject, clickPath, agent);
    }

    private void OnEnable()
    {
        unitClick.NewTargetAcquired += OnNewTargetAcquired;
    }

    private void OnDisable()
    {
        unitClick.NewTargetAcquired -= OnNewTargetAcquired;
    }

    private void Start()
    {
        ChangeState(startState);
    }

    public void OnNewTargetAcquired(Vector3 newTarget)
    {
        targetPosition = newTarget;
    }
}
