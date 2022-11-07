using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamage
{
    Animator animator;
    public StateManager stateManager;

    void Start()
    {
        StaringUp(stateManager);
    }

    public void StaringUp(StateManager stateManager)
    {
        animator = stateManager.animator;
    }

    public int health;
    public void takeDamage(int dmg)
    {
        animator.SetTrigger("hit");
        health -= dmg;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

   

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.takeDamage(1);
        }
    }
}
