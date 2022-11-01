using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolState : State
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] int pathingSpeed;
    public chaseState chase;
    float waypointDistance;
    int waypointIndex;
    public bool canSeeThePlayer;
   
    public override State runCurrentState()
    {
        if (canSeeThePlayer)
        {
            return chase;
        }
        else
            
         Patrol();
        if (waypointDistance < 1f)
        {
            increaceIndex();
        }
        return this;
    }
    void Patrol()
    {
        transform.Translate(Vector3.forward * pathingSpeed * Time.deltaTime);
    }
    void increaceIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }

}
