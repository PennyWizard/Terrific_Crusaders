using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    NavMeshAgent agent;
    public Animator animator;
    private int wayPointIndex;
    Vector3 target;


    public override void RunCurrentState(StateManager stateManager)
    {
        Debug.Log("Patroling");
        agent = stateManager.agent;
        UpdatDestination(stateManager);
        animator = stateManager.animator;
    }

    public override void UpdateState(StateManager stateManager)
    {

        if (!stateManager.canSeePlayer)
        {
            if (Vector3.Distance(stateManager.transform.position, target) < 1f)
            {
                animator.SetBool("seePlayer", false);
                IterateWaypointIndex(stateManager);
                UpdatDestination(stateManager);
            }
        }
        else
        {
            stateManager.SwitchStates(stateManager.chase);
        }

        
    }

    void UpdatDestination(StateManager stateManager)
    {
        Debug.Log("Moving");
        target = stateManager.waypoints[wayPointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex(StateManager stateManager)
    {
        Debug.Log("Update WayPoint");
        wayPointIndex++;
        if (wayPointIndex == stateManager.waypoints.Length)
        {
            wayPointIndex = 0;
        }
    }
}
