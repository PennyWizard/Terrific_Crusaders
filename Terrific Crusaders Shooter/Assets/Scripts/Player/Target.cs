using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamage
{
    public int health;
    public void takeDamage(int dmg)
    {
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
