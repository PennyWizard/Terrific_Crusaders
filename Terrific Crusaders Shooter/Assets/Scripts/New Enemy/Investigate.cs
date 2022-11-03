using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Investigate : State
{
    NavMeshAgent agent;

    public override void RunCurrentState(StateManager stateManager)
    {
        Debug.Log("Investigate");
        agent = stateManager.agent;
    }


    public override void UpdateState(StateManager stateManager)
    {
        
        Lookaround(stateManager);
    }

    void Lookaround(StateManager stateManager)
    {
        Debug.Log("Looking");
        agent.SetDestination(stateManager.sound1.pos);

        if (agent.remainingDistance <= 1f  && !stateManager.canSeePlayer)
        {
            stateManager.SwitchStates(stateManager.patrol);
        }
        else if (agent.remainingDistance <= 1f  && stateManager.canSeePlayer)
        {
            stateManager.SwitchStates(stateManager.chase);
        }
    }
}
