using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{

    [Header("---Component---")]
    [SerializeField] CharacterController controller;

    [Header("---Player Stats---")]
    [Range(1, 5)] [SerializeField] float playerSpeed;
    [SerializeField] float sprintMod;
    [Range(8, 15)] [SerializeField] float jumpHeight;
    [Range(15, 35)] [SerializeField] float gravityValue;
    [SerializeField] int jumpsMax;
    [Range(1, 20)] [SerializeField] int HP;

    [Header("---Gun Stats---")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shootDmg;
    [SerializeField] GameObject gunModel;
    [SerializeField] List<gunStats> gunStat = new List<gunStats>();

    [Header("---Audeo---")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] playerHurtAud;
    [Range(0, 1)] [SerializeField] float playerHurtAudVol;
    [SerializeField] AudioClip gunShootSound;
    [Range(0, 1)] [SerializeField] float playerShootAudVol;
    [SerializeField] AudioClip playerStepsAud;
    [Range(0, 1)] [SerializeField] float playerStepsAudVol;
    [SerializeField] AudioClip playerRespawnAud;
    [Range(0, 1)] [SerializeField] float playerRespawnAudVol;

    private Vector3 playerVelocity;
    private int timesJumped;
    public int currentAmmo;
    public int ammoMax;
    public float reloadTime = 2f;
    bool isShoot;
    int selectedgun;
    int hpOringal;
    bool isSprinting;
    bool isPlayingSteps;
    bool isReloading;
    Vector3 move;

    private void Start()
    {
        hpOringal = HP;
        Respawm();
    }

    void Update()
    {
        movement();
        StartCoroutine(playSteps());
        sprint();
        gunSelect();

        if (currentAmmo <= 0)
        {
            StartCoroutine(ReloadGun());
        }
        else
        {
            StartCoroutine(shoot());
        }
    }

    void movement()
    {
        //is on the ground, reset Y velocity, reset jump counter
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

        //first-person get inputs for ground movement
        move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Jumping
        if (Input.GetButtonDown("Jump") && timesJumped < jumpsMax)
        {
            timesJumped++;
            playerVelocity.y = jumpHeight;
        }

        //Gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            playerSpeed *= sprintMod;
            isSprinting = true;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            playerSpeed /= sprintMod;
            isSprinting = false;
        }
    }

    IEnumerator playSteps()
    {
        if (move.magnitude > 0.3f && !isPlayingSteps && controller.isGrounded)
        {
            isPlayingSteps = true;
            aud.PlayOneShot(playerStepsAud, playerStepsAudVol);

            if (isSprinting)
            {
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(0.6f);
            }
            
            isPlayingSteps = false;
        }
    }

    //
    IEnumerator shoot()
    {
        if(Input.GetButton("Shoot") && !isShoot)
        {
            isShoot = true;

            currentAmmo--;

            aud.PlayOneShot(gunShootSound, playerShootAudVol);

            RaycastHit hit;


            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)),out hit, shootDist))
            {
                if (hit.collider.GetComponent<IDamage>() != null)
                {
                    hit.collider.GetComponent<IDamage>().takeDamage(shootDmg);
                }

                
            }

            yield return new WaitForSeconds(shootRate);
            isShoot = false;
        }

    }

    IEnumerator ReloadGun()
    {
        Debug.Log("Reload...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoMax;

    }

    public void gunPickup(gunStats stats)
    {
        shootRate = stats.shootRate;
        shootDist = stats.shootDist;
        shootDmg = stats.shootDmg;
        ammoMax = stats.ammoMax;
        stats.currentAmmo = stats.ammoMax;
        currentAmmo = stats.currentAmmo;
        gunShootSound = stats.sound;


        gunModel.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        gunStat.Add(stats);
    }

    public void gunSelect()
    {
        if (gunStat.Count > 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedgun < gunStat.Count - 1)
            {
                gunStat[selectedgun].currentAmmo = currentAmmo;
                selectedgun++;
                changeGun();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedgun > 0)
            {
                gunStat[selectedgun].currentAmmo = currentAmmo;
                selectedgun--;
                changeGun();
            }
        }
    }

    public void changeGun()
    {
        shootRate = gunStat[selectedgun].shootRate;
        shootDist = gunStat[selectedgun].shootDist;
        shootDmg = gunStat[selectedgun].shootDmg;
        gunShootSound = gunStat[selectedgun].sound;
        ammoMax = gunStat[selectedgun].ammoMax;
        currentAmmo = gunStat[selectedgun].currentAmmo;
        

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectedgun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectedgun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;

    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        aud.PlayOneShot(playerHurtAud[Random.Range(0, playerHurtAud.Length - 1)], playerHurtAudVol);

        updatePlayerHUD();

        StartCoroutine(GameManager.instance.playerDamage());

        if (HP <= 0)
        {
            GameManager.instance.Menu.SetActive(true);
            GameManager.instance.respawnButton.SetActive(true);
            GameManager.instance.curserLock();
            GameManager.instance.isMenuOpen = true;

        }
    }

    public void updatePlayerHUD()
    {
        GameManager.instance.playerHPBar.fillAmount = (float)HP / (float)hpOringal;

        
    }

    public void Respawm()
    {
        //controller.enabled = false;

        HP = hpOringal;

        aud.PlayOneShot(playerRespawnAud, playerRespawnAudVol);

        updatePlayerHUD();
        transform.position = GameManager.instance.spawnPosition.transform.position;

        controller.enabled = true;
    }

}