using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("---Gun Stats---")]
    public int damage;
    public float range;
    public int ammoMax;
    public int currentAmmo;
    public float shootRate;

    public bool isShoot;
    bool isReloading;

    [Header("---Audeo---")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip gunShootSound;
    [Range(0, 1)] [SerializeField] float playerShootAudVol;
    [SerializeField] AudioClip playerReloadAud;
    [Range(0, 1)] [SerializeField] float playerReloadAudVol;
    [SerializeField] AudioClip gunEmptyAud;
    [Range(0, 1)] [SerializeField] float gunEmptyAudVol;
    public float soundRange;
    [SerializeField]ParticleSystem muzzleFlash;

    

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
}
