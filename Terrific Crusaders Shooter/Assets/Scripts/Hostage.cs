using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage : InteractableBase
{
    public NavMeshAgent agent;
    public GameManager gameManager;
    public Animator animator;
    public GameObject waypoint;
    bool freed;

    private void Start()
    {
        GameManager.instance.hostageAmount++;
    }
    public override void OnInteract()
    {

        if (!gameManager.isPaused)
        {
            if (!freed)
            {
                freed = true;
                animator.SetBool("free", true);
                agent.SetDestination(waypoint.transform.position);

                GameManager.instance.hostageCurrent++;

                GameManager.instance.updateText();
            }
        }
    }

}
