using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour, IDamage
{
    Animator animator;
    public StateManager stateManager;
    public bool hasKey;
    public GameObject key;
    public bool isDead;
    //public Collider collider;


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
        if (!isDead)
        {
            animator.SetTrigger("hit");
            health -= dmg;

            if (health <= 0)
            {
                //collider.enabled = false;
                isDead = true;

                if (hasKey)
                {
                    Vector3 position = transform.position;
                    Instantiate(key, position, Quaternion.identity);
                }

                Death();

            }
        }

    }

    public void Death()
    {
        animator.SetTrigger("die");
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        

        Destroy(gameObject, 3f);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.takeDamage(1);
        }
    }
}
