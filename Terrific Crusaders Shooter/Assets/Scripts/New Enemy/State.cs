using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void RunCurrentState(StateManager stateManager);
    public abstract void UpdateState(StateManager stateManager);
}
