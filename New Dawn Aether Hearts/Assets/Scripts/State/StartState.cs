using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StartState : IState
{
    StateCycle stateCycle;
    UnitClick unitClick;

    public StartState(StateCycle stateCycle, UnitClick unitClick)
    {
        this.stateCycle = stateCycle;
        this.unitClick = unitClick;
    }

    public void Enter()
    {
        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", true);
        }
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
        if (stateCycle.GetComponent<Animator>())
        {
            stateCycle.GetComponent<Animator>().SetBool("isStart", false);
        }
    }

    public void OnNewTargetAcquired(Vector3 newPosition)
    {
        if (stateCycle.isSelected)
        {
            stateCycle.ChangeState(stateCycle.moveState);
        }
    }
}
