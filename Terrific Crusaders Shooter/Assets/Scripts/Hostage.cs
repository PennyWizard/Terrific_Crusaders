using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage : InteractableBase
{
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject waypoint;

    private void Start()
    {
        GameManager.instance.hostageAmount++;
    }
    public override void OnInteract()
    {
        
        animator.SetBool("free", true);
        agent.SetDestination(waypoint.transform.position);

        GameManager.instance.hostageCurrent++;

        GameManager.instance.updateText();
    }

}
