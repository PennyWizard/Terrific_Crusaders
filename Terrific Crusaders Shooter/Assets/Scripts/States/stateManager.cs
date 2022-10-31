using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stateManager : MonoBehaviour
{
    State currentState;
    void Update()
    {
        runStateMachine();
    }
    private void runStateMachine()
    {
        State nextState = currentState?.runCurrentState();

        if(nextState != null)
        {
            switchToNextState(nextState);
        }
    }
    private void switchToNextState(State nextState)
    {
        currentState = nextState;
    } 
    
}
