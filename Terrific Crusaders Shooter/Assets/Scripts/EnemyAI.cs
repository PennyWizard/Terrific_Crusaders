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
    [SerializeField] Animator animator;
    [SerializeField] int animationLerpSpeed;
    [SerializeField] Collider col;
    [SerializeField] Transform[] waypoints;


    [Header("---Enemy Stats--")]
    [Range(0, 100)][SerializeField] int HP;
    [Range(0, 20)][SerializeField] int facePlayerSpeed;
    [SerializeField] int sightDist;
    [Range(0, 90)][SerializeField] int viewAngle;
    [SerializeField] bool burstEnemy;
    [Range(0, 1)][SerializeField] float enemyBurstSpeed;
    [SerializeField]int pathingSpeed;

    [Header("---Roam info---")]
    [Range(0, 100)][SerializeField] int roamDistance;

    [Header("--- Gun Stats---")]
    [Range(1, 25)][SerializeField] int shootDMG;
    [Range(0, 1)][SerializeField] float rateOfFire;
    [Range(0, 1)][SerializeField] float shootPause;

    [Header("--- Audio ---")]
    [SerializeField] AudioSource enemyAudio;
    [SerializeField] AudioClip[] enemyHurt;
    [SerializeField] AudioClip[] enemySteps;
    [SerializeField] AudioClip enemyShots;
    [SerializeField] AudioClip enemyBurst;
    [Range(0,1)][SerializeField] float enemyGunVol;
    [Range(0, 1)][SerializeField] float enemyStepsVol;
    [Range(0, 1)][SerializeField] float enemyHurtVol;

    public bool playerInRange;
    public bool isShooting;

    Vector3 playerDirection;
    Vector3 startingPosition;

    float stoppingDistanceOrgin;
    float angle;
    float patrolSpeed;
    float waypointDistance;

    [SerializeField]int waypointIndex;

    void Start()
    {
        stoppingDistanceOrgin = agent.stoppingDistance;
        startingPosition = transform.position;
        patrolSpeed = agent.speed;
        transform.LookAt(waypoints[waypointIndex].position);
        waypointIndex = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!animator.GetBool("Dead"))
        //{
                 Patrol();

             //if (agent.enabled && !playerInRange)
             //{
                 waypointDistance = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
                 animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"),agent.velocity.normalized.magnitude, Time.deltaTime * animationLerpSpeed));
                 if (waypointDistance < 1f)
                 {
                     increaceIndex();
                 }      
             //}
           

             if (agent.enabled && playerInRange)
             {
                 playerDirection = GameManager.instance.player.transform.position - headPos.transform.position;
                 angle = Vector3.Angle(playerDirection, transform.forward);
                 canSeePlayer();
             
                 if (agent.remainingDistance < 0.1f && agent.destination != GameManager.instance.player.transform.position && !playerInRange)
                 {
                    
                    StartCoroutine(roam());
                    
                 }
                 
             
             }
        //}
    }
    public void takeDamage(int damage)
    {
        if (damage > 0)
        {
            HP -= damage;
            StartCoroutine(damageFeedback());

        }
        else if (damage < 0)
        {
            HP -= damage;
            StartCoroutine(healFeedback());
        }
        if (HP <= 0)
        {
            animator.SetBool("Dead", true);
            agent.enabled = false;
            col.enabled = false;
           GameManager.instance.checkEnemyTotal();
        }
    }
    IEnumerator damageFeedback()
    {
       animator.SetTrigger("Damage");
       model.material.color = Color.red;
       enemyAudio.PlayOneShot(enemyHurt[Random.Range(0, enemyHurt.Length - 1)], enemyHurtVol);
       agent.enabled = false;

       yield return new WaitForSeconds(0.1f);

        model.material.color = Color.white;
        if (!animator.GetBool("Dead"))
        {
            agent.enabled = true;
            agent.SetDestination(GameManager.instance.player.transform.position);
        }
    }
    IEnumerator healFeedback()
    {
        animator.SetTrigger("Damage");
        model.material.color = Color.green;
        agent.enabled = false;
        enemyAudio.PlayOneShot(enemyHurt[Random.Range(0, enemyHurt.Length - 1)], enemyHurtVol);
        yield return new WaitForSeconds(0.1f);

        model.material.color = Color.white;
        agent.enabled = true;
    }
    IEnumerator shoot()
    {
        isShooting = true;
        if (!burstEnemy)
        {
            animator.SetTrigger("Shoot");
            for (int i = 0; i < 7; i++)
            {
                enemyAudio.PlayOneShot(enemyShots, enemyGunVol);
                Instantiate(bullet, shootPosition.transform.position, transform.rotation);
                yield return new WaitForSeconds(rateOfFire);
            }
            yield return new WaitForSeconds(shootPause);
        }
        else
        {
            animator.SetTrigger("Burst");
            for (int i = 0; i < 3; i++)
            {
                enemyAudio.PlayOneShot(enemyShots, enemyGunVol);
                Instantiate(bullet, shootPosition.transform.position, transform.rotation);
                yield return new WaitForSeconds(enemyBurstSpeed);

            }
        }


        yield return new WaitForSeconds(rateOfFire);
        isShooting = false;
    }
    
   
    
    IEnumerator roam()
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
        yield return new WaitForSeconds(1.5f);
    }
    void facePlayer()
    {
        playerDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * facePlayerSpeed);
        
    }
    void canSeePlayer()
    {
        if (playerInRange)
        {
            
            RaycastHit hit;
            
            if (Physics.Raycast(headPos.transform.position, playerDirection, out hit, sightDist))
            {
                Debug.DrawRay(headPos.transform.position, playerDirection);

                if (hit.collider.CompareTag("Player") && angle <= viewAngle && playerInRange)
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
    IEnumerator enemyPlaySteps()
    {
        if(agent.speed > 0)
        {
            enemyAudio.PlayOneShot(enemySteps[Random.Range(0, enemySteps.Length - 1)], enemyStepsVol);
            yield return new WaitForSeconds(0.4f);
        }
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
