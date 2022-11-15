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
    public AudioSource source;
    public AudioClip[] spoted;
    public bool playing;

    public override void RunCurrentState(StateManager stateManager)
    {
        Debug.Log("Chashing");

        agent = stateManager.agent;
        range = stateManager.shootRange;
        damage = stateManager.Damage;
        animator = stateManager.animator;

        animator.SetBool("seePlayer", true);
        stateManager.playSound();

    }

    public override void UpdateState(StateManager stateManager)
    {
        if (stateManager.canSeePlayer)
        {
            
            if (stateManager.isInRange)
            {
                agent.SetDestination(stateManager.player.transform.position);
                lastKnow = stateManager.player.transform.position;
                stateManager.lookAtPlayer();
                agent.isStopped = true;
                stateManager.ShootGun();
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(stateManager.player.transform.position);
                lastKnow = stateManager.player.transform.position;
            }
        }
        else
        {
            agent.SetDestination(lastKnow);
            agent.isStopped = false;

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
