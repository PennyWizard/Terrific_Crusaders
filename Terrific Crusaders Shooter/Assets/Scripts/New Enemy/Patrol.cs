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
    public AudioSource source;
    public AudioClip[] allClear;


    public override void RunCurrentState(StateManager stateManager)
    {
        agent = stateManager.agent;
        animator = stateManager.animator;
        animator.SetBool("seePlayer", false);
        source = stateManager.source;
        allClear = stateManager.allClear;

        UpdatDestination(stateManager);
        source.PlayOneShot(allClear[Random.Range(0, allClear.Length - 1)]);    
        
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
        
        target = stateManager.waypoints[wayPointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex(StateManager stateManager)
    {
        
        wayPointIndex++;
        if (wayPointIndex == stateManager.waypoints.Length)
        {
            wayPointIndex = 0;
        }
    }
}
