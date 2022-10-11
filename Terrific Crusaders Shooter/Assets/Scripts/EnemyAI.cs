using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour, IDamage
{
    [Header("--- Component ---")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] GameObject shootPosition;
    [SerializeField] GameObject bullet;

    [Header("---Enemy Stats--")]
    [Range(0, 100)][SerializeField] int HP;
    [Range(0, 20)][SerializeField] int facePlayerspeed;
    [SerializeField] int sightDist;

    [Header("--- Gun Stats---")]
    [Range(1, 25)][SerializeField] int shootDMG;
    [Range(0, 10)][SerializeField] float rateOfFire;

    public bool playerInRange;
    public bool isShooting;
    void Start()
    {
        GameManager.instance.enemyAmount++;
       

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled && playerInRange)
        {
            agent.SetDestination(GameManager.instance.player.transform.position);
            if (!isShooting)
            {
                
                StartCoroutine(shoot());
            }
        }

    }
    public void takeDamage(int damage)
    {
        HP -= damage;
        StartCoroutine(damageFeedback());
        if (HP <= 0)
        {
           GameManager.instance.checkEnemyTotal();
            Destroy(gameObject);
        }
    }
    IEnumerator damageFeedback()
    {
        model.material.color = Color.red;
        agent.enabled = false;

        yield return new WaitForSeconds(0.1f);

        model.material.color = Color.white;
        agent.enabled = true;
    }
    IEnumerator shoot()
    {
        isShooting = true;

        Instantiate(bullet, shootPosition.transform.position, transform.rotation);

        yield return new WaitForSeconds(rateOfFire);
        isShooting = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
   }
