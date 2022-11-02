using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FuntionalAdult : MonoBehaviour, IHear
{
    [SerializeField] NavMeshAgent agent = null;
    private float displacementFromDanger = 10f;

    void Awake()
    {
        if (!TryGetComponent(out agent))
        {
            Debug.LogWarning("No NavMeshAgent Fond!");
        }
    }

   public void RespondToSound(Sound sound)
    {
        if (sound.soundType == Sound.SoundType.Intersting)
        {
            MoveTo(sound.pos);
        }
        else if (sound.soundType == Sound.SoundType.Danger)
        {
            Vector3 dir = (sound.pos - transform.position).normalized;
            MoveTo(transform.position - (dir * displacementFromDanger));
        }
        Debug.Log("Heared Sound");
    }

    private void MoveTo(Vector3 pos)
    {
        agent.SetDestination(pos);
        agent.isStopped = false;
    }
}
