using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    NavMeshAgent agent;
    float range;
    int damage;
    public Animator animator;
    Vector3 lastKnow;


    public override void RunCurrentState(StateManager stateManager)
    {
        Debug.Log("Chashing");

        agent = stateManager.agent;
        range = stateManager.shootRange;
        damage = stateManager.Damage;
        animator = stateManager.animator;

        animator.SetBool("seePlayer", true);
    }

    public override void UpdateState(StateManager stateManager)
    {
        if (stateManager.canSeePlayer)
        {
            //animator.SetBool("seePlayer", true);

            if (stateManager.isInRange)
            {
                agent.SetDestination(stateManager.player.transform.position);
                lastKnow = stateManager.player.transform.position;
                stateManager.ShootGun();
            }
            else
            {
                agent.SetDestination(stateManager.player.transform.position);
                lastKnow = stateManager.player.transform.position;
            }
        }
        else
        {
            agent.SetDestination(lastKnow);

            if (agent.remainingDistance <= 1f && !stateManager.canSeePlayer)
            {
                animator.SetBool("hearSomething", false);
                stateManager.SwitchStates(stateManager.patrol);
            }
            else if (agent.remainingDistance <= 1f && stateManager.canSeePlayer)
            {
                animator.SetBool("hearSomething", false);
                stateManager.SwitchStates(stateManager.chase);
            }
            
        }
    }

    
    
}
