using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : IState
{
    StateCycle stateCycle;
    UnitClick unitClick;

    ClickPath clickPath;
    Subject subject;

    public StartState(StateCycle stateCycle, Subject subject, ClickPath clickPath, UnitClick unitClick)
    {
        this.stateCycle = stateCycle;
        this.subject = subject;
        this.clickPath = clickPath;
        this.unitClick = unitClick;
    }

    public void Enter()
    {
        unitClick.NewTargetAcquired += OnNewTargetAcquired;
    }

    public void Tick()
    {

    }

    public void FixedTick()
    {

    }

    public void Exit()
    {
        unitClick.NewTargetAcquired -= OnNewTargetAcquired;
    }

    public void OnNewTargetAcquired(Vector3 newPosition)
    {
        if (stateCycle.isSelected)
        {
            stateCycle.ChangeState(stateCycle.moveState);
        }
    }
}
