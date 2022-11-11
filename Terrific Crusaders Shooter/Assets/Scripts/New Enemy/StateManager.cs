using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour, IHear
{
    [Header("State")]
    public NavMeshAgent agent;
    public State currentState;
    public Patrol patrol = new Patrol();
    public Chase chase = new Chase();
    public Investigate investigate = new Investigate();
    public Animator animator;
    

    [Header("Field of View")]
    public float radius;
    public float angle;
    public bool canSeePlayer;
    public GameObject player;
    public LayerMask playerMask;
    public LayerMask obstructionMask;


    [Header("Sound")]
    public Sound sound1;
    //private float displacementFromDanger = 10f;

    [Header("Patrol")]
    public Transform[] waypoints;

    [Header("Chase")]
    public float shootRange;
    public bool isInRange;
    public bool isShooting;
    public int Damage;
    public GameObject shootPoint;

    [Header("---Juice--")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] TrailRenderer tracer;
    [SerializeField] Transform tracerSpawn;


    void Start()
    {
        currentState = patrol;
        currentState.RunCurrentState(this);
        StartCoroutine(FOVroutine());

    }

    void Update()
    {
        currentState.UpdateState(this);
        StartCoroutine(FOVroutine());
        InRangeCheck();
    }

    public void SwitchStates(State state)
    {
        currentState = state;
        state.RunCurrentState(this);
    }

    IEnumerator FOVroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            
        }
    }

    void FieldOfViewCheck()
    {
        Collider[] rangerCheck = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangerCheck.Length != 0)
        {
            Transform target = rangerCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
        }
    }

    


    void InRangeCheck()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= shootRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }

    public void RespondToSound(Sound sound)
    {
        if (sound.soundType == Sound.SoundType.Intersting)
        {
            sound1 = sound;
            this.SwitchStates(investigate);
        }
        
        
    }

    public void ShootGun()
    {
        if (!isShooting && canSeePlayer)
        {
            StartCoroutine(Fire());
            
        }
        this.SwitchStates(chase);
    }
    
    IEnumerator Fire()
    {
        isShooting = true;

        Vector3 directionToTarget = (player.transform.position - shootPoint.transform.position).normalized;
        float distanceToTarget = Vector3.Distance(shootPoint.transform.position, player.transform.position);

        muzzleFlash.Play();

        if (!Physics.Raycast(shootPoint.transform.position, directionToTarget, distanceToTarget, obstructionMask) && Vector3.Angle(transform.forward, directionToTarget) < 45)
        {
            GameManager.instance.playerScript.takeDamage(Damage);
        }
        if (Physics.Raycast(tracerSpawn.position, transform.forward, out RaycastHit hit2, shootRange))
        {
            tracer.emitting = true;
            TrailRenderer tracerTrail = Instantiate(tracer, tracerSpawn.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(tracerTrail, hit2));
            tracer.emitting = false;

        }
        yield return new WaitForSeconds(1f);
        
        
        isShooting = false;
    }
    private IEnumerator SpawnTrail(TrailRenderer tracerTrail, RaycastHit hit)
    {
        float time = 0;
        Vector3 trailStartPosition = tracerTrail.transform.position;

        while (time < 1)
        {
            tracerTrail.transform.position = Vector3.Lerp(trailStartPosition, hit.point, time);
            time += Time.deltaTime / tracerTrail.time;

            yield return null;
        }
        tracerTrail.transform.position = hit.point;
        //Instantiate(shotHit,hit.point,Quaternion.LookRotation(hit.normal));

        Destroy(tracerTrail.gameObject, tracerTrail.time);
    }
}
