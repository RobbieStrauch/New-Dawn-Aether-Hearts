using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.youtube.com/watch?v=dWrRb9pO8aQ&t=932s

public interface IState
{
    void Enter();
    void Tick();
    void FixedTick();
    void Exit();
}
