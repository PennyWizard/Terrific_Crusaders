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
    [Range(1, 20)] public int HP;
    public bool hasKey;
    public bool hasKey2;

    [Header("---Audeo---")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] playerHurtAud;
    [Range(0, 1)] [SerializeField] float playerHurtAudVol;
    [SerializeField] AudioClip playerStepsAud;
    [Range(0, 1)] [SerializeField] float playerStepsAudVol;
    [SerializeField] AudioClip playerRespawnAud;
    [Range(0, 1)] [SerializeField] float playerRespawnAudVol;

    private Vector3 playerVelocity;
    private int timesJumped;
    int hpOringal;
    bool isSprinting;
    bool isPlayingSteps;
    
    

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
        controller.enabled = false;

        HP = hpOringal;

        aud.PlayOneShot(playerRespawnAud, playerRespawnAudVol);

        updatePlayerHUD();
        transform.position = GameManager.instance.spawnPosition.transform.position;

        controller.enabled = true;
    }

}