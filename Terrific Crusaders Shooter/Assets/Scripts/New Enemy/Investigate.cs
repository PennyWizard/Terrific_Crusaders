using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Investigate : State
{
    NavMeshAgent agent;
    public Animator animator;

    public override void RunCurrentState(StateManager stateManager)
    {
        
        agent = stateManager.agent;
        animator = stateManager.animator;

        animator.SetBool("hearSomething", true);
    }


    public override void UpdateState(StateManager stateManager)
    {
        //animator.SetBool("hearSomething", true);
        Lookaround(stateManager);
        
    }

    void Lookaround(StateManager stateManager)
    {
       
        agent.SetDestination(stateManager.sound1.pos);

        if (agent.remainingDistance <= 1f  && !stateManager.canSeePlayer)
        {
            animator.SetBool("hearSomething", false);
            stateManager.SwitchStates(stateManager.patrol);
        }
        else if (agent.remainingDistance <= 1f  && stateManager.canSeePlayer)
        {
            animator.SetBool("hearSomething", false);
            stateManager.SwitchStates(stateManager.chase);
        }
    }
}
