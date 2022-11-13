using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Investigate : State
{
    NavMeshAgent agent;
    public Animator animator;
    public AudioSource source;
    public AudioClip[] what;

    public override void RunCurrentState(StateManager stateManager)
    {
        
        agent = stateManager.agent;
        animator = stateManager.animator;
        source = stateManager.source;
        what = stateManager.what;

        animator.SetBool("hearSomething", true);
        source.PlayOneShot(what[Random.Range(0, what.Length - 1)]);
    }


    public override void UpdateState(StateManager stateManager)
    {
        //animator.SetBool("hearSomething", true);
        Lookaround(stateManager);
        
    }

    void Lookaround(StateManager stateManager)
    {
       
        agent.SetDestination(stateManager.sound1.pos);

        if (!stateManager.canSeePlayer)
        {
            if (agent.remainingDistance <= 1f && !stateManager.canSeePlayer) //
            {
                animator.SetBool("hearSomething", false);
                stateManager.SwitchStates(stateManager.patrol);
            }
            else if (agent.remainingDistance <= 1f && stateManager.canSeePlayer) //
            {
                animator.SetBool("hearSomething", false);
                stateManager.SwitchStates(stateManager.chase);
            }
        }
        else
        {
            animator.SetBool("hearSomething", false);
            stateManager.SwitchStates(stateManager.chase);
        }
    }
}
