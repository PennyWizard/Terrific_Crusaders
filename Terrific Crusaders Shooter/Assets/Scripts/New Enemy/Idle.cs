using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    NavMeshAgent agent;
    public Animator animator;
    Vector3 target;

    public override void RunCurrentState(StateManager stateManager)
    {
        //Debug.Log("Idle State");
        agent = stateManager.agent;
        animator = stateManager.animator;

    }

    public override void UpdateState(StateManager stateManager)
    {
        if (stateManager.canSeePlayer)
        {
            animator.SetBool("isIdle", false);
            stateManager.SwitchStates(stateManager.chase);
        }
        else
        {
            UpdatDestination(stateManager);
            if (agent.remainingDistance <= 1f)
            {
                //Debug.Log("idling");
                animator.SetBool("isIdle", true);
            }
            
        }
    }

    void UpdatDestination(StateManager stateManager)
    {
        //Debug.Log("Moving to idle");
        target = stateManager.waypoints[0].position;
        agent.SetDestination(target);
    }
}
