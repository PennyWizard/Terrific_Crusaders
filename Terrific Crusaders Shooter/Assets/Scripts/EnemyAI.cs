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
    [SerializeField] GameObject headPos;
    [SerializeField] GameObject bullet;

    [Header("---Enemy Stats--")]
    [Range(0, 100)][SerializeField] int HP;
    [Range(0, 20)][SerializeField] int facePlayerSpeed;
    [SerializeField] int sightDist;
    [Range(0, 90)][SerializeField] int viewAngle;

    [Header("---Roam info---")]
    [Range(0, 100)][SerializeField] int roamDistance;

    [Header("--- Gun Stats---")]
    [Range(1, 25)][SerializeField] int shootDMG;
    [Range(0, 10)][SerializeField] float rateOfFire;

    public bool playerInRange;
    public bool isShooting;
    Vector3 playerDirection;
    Vector3 startingPosition;
    float stoppingDistanceOrgin;
    float angle;
    float patrolSpeed;
    void Start()
    {
        GameManager.instance.enemyAmount++;
        stoppingDistanceOrgin = agent.stoppingDistance;
        startingPosition = transform.position;
        patrolSpeed = agent.speed;
        roam();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            
            if (playerInRange)
            {
                playerDirection = GameManager.instance.player.transform.position - headPos.transform.position;
                angle = Vector3.Angle(playerDirection, transform.forward);
                canSeePlayer();

            }
            if (agent.remainingDistance < 0.1f && agent.destination != GameManager.instance.player.transform.position)
            {
                roam();
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
    
    void roam()
    {
        agent.stoppingDistance = 0;
        agent.speed = patrolSpeed;

        Vector3 randomDirection = Random.insideUnitSphere * roamDistance;
        randomDirection += startingPosition;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 1, 1);
        NavMeshPath path = new NavMeshPath();

        if (hit.position != null)
        {
            agent.CalculatePath(hit.position, path);
            agent.SetPath(path);
        }

    }
    void facePlayer()
    {
        playerDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * facePlayerSpeed);
        
    }
    void canSeePlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(headPos.transform.position, playerDirection, out hit, sightDist))
        {
            Debug.DrawRay(headPos.transform.position, playerDirection);

            if (hit.collider.CompareTag("Player") && angle <= viewAngle)
            {
                agent.stoppingDistance = stoppingDistanceOrgin;
                agent.SetDestination(GameManager.instance.player.transform.position);
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    facePlayer();
                }

                if (!isShooting)
                {
                    StartCoroutine(shoot());
                }

            }
            
        
        }
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
