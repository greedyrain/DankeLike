using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    IState currentState;

    private void Update()
    {
        currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    public void SwitchState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = state;
        currentState.OnEnter();
    }
}
