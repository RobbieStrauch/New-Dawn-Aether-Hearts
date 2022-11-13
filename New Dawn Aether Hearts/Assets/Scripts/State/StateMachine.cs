using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.youtube.com/watch?v=dWrRb9pO8aQ&t=932s

public class StateMachine : MonoBehaviour
{
    public IState currentState { get; private set; }
    public IState previousState;

    bool inTransition = false;

    public void ChangeState(IState newState)
    {
        if (currentState == newState || inTransition)
        {
            return;
        }

        ChangeStateRoutine(newState);
    }

    public void RevertState()
    {
        if (previousState != null)
        {
            ChangeState(previousState);
        }
    }

    public void ChangeStateRoutine(IState newState)
    {
        inTransition = true;

        if (currentState != null)
        {
            currentState.Exit();
        }

        if (previousState != null)
        {
            previousState = currentState;
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.Enter();
        }

        inTransition = false;
    }

    public void Update()
    {
        if (currentState != null && !inTransition)
        {
            currentState.Tick();
        }
    }

    public void FixedUpdate()
    {
        if (currentState != null && !inTransition)
        {
            currentState.FixedTick();
        }
    }
}
