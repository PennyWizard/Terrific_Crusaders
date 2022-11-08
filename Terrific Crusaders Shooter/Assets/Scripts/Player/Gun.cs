using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("---Gun Stats---")]
    public int damage;
    public float range;
    public int ammoMax;
    public int currentAmmo;
    public float shootRate;
    public int tracerTrigger;

    public bool isShoot;
    bool isReloading;

    [Header("---Audio---")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip gunShootSound;
    [Range(0, 1)] [SerializeField] float playerShootAudVol;
    [SerializeField] AudioClip playerReloadAud;
    [Range(0, 1)] [SerializeField] float playerReloadAudVol;
    [SerializeField] AudioClip gunEmptyAud;
    [Range(0, 1)] [SerializeField] float gunEmptyAudVol;
    public float soundRange;

    [Header("--Effects--")]
    [SerializeField]ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem shotHit;
    [SerializeField] TrailRenderer tracer;
    [SerializeField] Transform tracerSpawn;
    [SerializeField] LayerMask hitLayer;
    

    // Update is called once per frame
    void Update()
    {
        if (currentAmmo > 0)
        {
            
            StartCoroutine(shoot());
        }
        else
        {
            StartCoroutine(gunEmpty());
        }

        ReloadGun();
    }

    IEnumerator shoot()
    {
        if (Input.GetButton("Shoot") && !isShoot && !isReloading)
        {
            isShoot = true;
            
            currentAmmo--;

            RaycastHit hit;

            

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, range))
            {
                

                if (hit.collider.GetComponent<IDamage>() != null)
                {
                    hit.collider.GetComponent<IDamage>().takeDamage(damage);
                    
                }


            }
            
            if(Physics.Raycast(tracerSpawn.position,transform.forward, out RaycastHit hit2, range)) {
                tracer.emitting = true;
                TrailRenderer tracerTrail = Instantiate(tracer, tracerSpawn.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(tracerTrail, hit));
                tracer.emitting = false;

            }
          
            muzzleFlash.Play();
            aud.PlayOneShot(gunShootSound, playerShootAudVol);
            MakeASound(soundRange);


            yield return new WaitForSeconds(shootRate);
            isShoot = false;
        }

    }

    IEnumerator gunEmpty()
    {
        if (Input.GetButton("Shoot") && !isShoot && !isReloading)
        {
            isShoot = true;

            aud.PlayOneShot(gunEmptyAud, gunEmptyAudVol);

            yield return new WaitForSeconds(shootRate);
            isShoot = false;
        }

    }

    void ReloadGun()
    {
        if (Input.GetButtonDown("Reload") && !isReloading)
        {
            isReloading = true;
            aud.PlayOneShot(playerReloadAud, playerReloadAudVol);
            currentAmmo = ammoMax;
            isReloading = false;
        }

    }

    public void MakeASound(float range)
    {
        var sound = new Sound(transform.position, range);
        sound.soundType = Sound.SoundType.Intersting;

        Sounds.MakeSound(sound);
    }
    private IEnumerator SpawnTrail(TrailRenderer tracerTrail, RaycastHit hit)
    {
        float time = 0;
        Vector3 trailStartPosition = tracerTrail.transform.position;

        while(time <1)
        {
            tracerTrail.transform.position = Vector3.Lerp(trailStartPosition, hit.point, time);
            time += Time.deltaTime / tracerTrail.time;

            yield return null;
        }
        tracerTrail.transform.position = hit.point;
        //Instantiate(shotHit,hit.point,Quaternion.LookRotation(hit.normal));

        Destroy(tracerTrail.gameObject,tracer.time);
    }
}

