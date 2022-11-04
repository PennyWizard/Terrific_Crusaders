using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    NavMeshAgent agent;
    float range;
    int damage;
    

    public override void RunCurrentState(StateManager stateManager)
    {
        Debug.Log("Chashing");

        agent = stateManager.agent;
        range = stateManager.shootRange;
        damage = stateManager.Damage;
    }

    public override void UpdateState(StateManager stateManager)
    {
        if (stateManager.canSeePlayer)
        {
            if (stateManager.isInRange)
            {
                agent.SetDestination(stateManager.player.transform.position);
                stateManager.ShootGun();
            }
            else
            {
                agent.SetDestination(stateManager.player.transform.position);
            }
        }
        else
        {
            stateManager.SwitchStates(stateManager.patrol);
        }
    }

    
    
}