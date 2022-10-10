using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("---Component---")]
    [SerializeField] CharacterController controller;

    [Header("---Player Stats---")]
    [Range(1, 5)] [SerializeField] float playerSpeed;
    [Range(8, 15)] [SerializeField] float jumpHeight;
    [Range(15, 35)] [SerializeField] float gravityValue;
    [SerializeField] int jumpsMax;
    [SerializeField] int HP;

    [Header("---Gun Stats---")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shootDmg;
    [SerializeField] GameObject gunModel;
    [SerializeField] List<gunStats> gunStat = new List<gunStats>();

    [SerializeField] GameObject cube;
    private Vector3 playerVelocity;
    private int timesJumped;
    bool isShoot;
    int selectedgun;
    int hpOringal;

    private void Start()
    {
        Respawm();
    }

    void Update()
    {
        movement();
        StartCoroutine(shoot());
        gunSelect();
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
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
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

    //
    IEnumerator shoot()
    {
        if(Input.GetButton("Shoot") && !isShoot)
        {
            isShoot = true;

            RaycastHit hit;


            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)),out hit, shootDist))
            {
                if (hit.collider.GetComponent<IDamage>() != null)
                {
                    hit.collider.GetComponent<IDamage>().takeDamage(shootDmg);
                }

                //Creat Cube
                //
                //Instantiate(cube, hit.point, cube.transform.rotation);

                //
                //Instantiate(cube, hit.point, Camera.main.transform.rotation);
                //
                //Instantiate(cube, hit.point, transform.parent.rotation);
            }

            yield return new WaitForSeconds(shootRate);
            isShoot = false;
        }

    }

    public void gunPickup(gunStats stats)
    {
        shootRate = stats.shootRate;
        shootDist = stats.shootDist;
        shootDmg = stats.shootDmg;
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
                selectedgun++;
                changeGun();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedgun > 0)
            {
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

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectedgun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectedgun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;

    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        updatePlayerHUD();

        //StartCoroutine(GameManager.instance.playerDamage());

        if (HP <= 0)
        {
            //GameManager.instance.playerDeadMenu.SetActive(true);
            // GameManager.instance.curserLockPause();

            /*Commented out above coed to clear up code to work with the code.
        without some of the UI elements Unity will not let the player move*/

        }
    }

    public void updatePlayerHUD()
    {
        //GameManager.instance.playerHPBar.fillAmount = (float)HP / (float)hpOringal;

        /*Commented out above coed to clear up code to work with the code.
        without some of the UI elements Unity will not let the player move*/
    }

    public void Respawm()
    {
        controller.enabled = false;
        //GameManager.instance.playerDeadMenu.SetActive(false);
        
        /*Commented out above coed to clear up code to work with the code.
         without some of the UI elements Unity will not let the player move*/

        HP = hpOringal;
        updatePlayerHUD();
        //transform.position = GameManager.instance.spawnPosition.transform.position;
        controller.enabled = true;
    }

}