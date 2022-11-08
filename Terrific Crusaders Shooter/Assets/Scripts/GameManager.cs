using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public InteractionInputData interactionInputData;
    public Gun gun;

    [Header("---Player Stuff---")]
    public GameObject player;
    public PlayerController playerScript;
    public cameraControlls cameraControlls;
    public CameraShake cameraShake;

    public GameObject spawnPosition;

    [Header("---UI---")]
    public GameObject Menu;
    public GameObject playerDamageFlash;
    public Image playerHPBar;
    public Text enemyCountText;
    public GameObject resumButton;
    public GameObject respawnButton;
    public GameObject youWinText;
    public GameObject youLoseText;
    public GameObject Introduction;
    public Text ammoCurrent;
    public Text ammoMax;
    public Text hostageCountText;
    public Text hostageCurrentText;

    

    public bool isPaused;
    public bool isMenuOpen;

    public int enemyAmount;
    public int hostageAmount = 0;
    public int hostageCurrent = 0;
    private int tempHostage = 5;

    //    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        spawnPosition = GameObject.FindGameObjectWithTag("Spawn Position");

    }

    void Start()
    {
        interactionInputData.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        enemyHostageCountWait();

        if (Input.GetButtonDown("Cancel") && !isMenuOpen)
        {
            isPaused = !isPaused;

            Menu.SetActive(isPaused);

            if (isPaused)
            {
                curserLock();
                Time.timeScale = 0f;
                gun.isShoot = true;
                resumButton.SetActive(true);
            }
            else
            {
                curserUnlock();
                Time.timeScale = 1f;
                gun.isShoot = false;
                resumButton.SetActive(false);
            }
        }

        

        ammoCurrent.text = gun.currentAmmo.ToString();
        ammoMax.text = gun.ammoMax.ToString();

    }

    void Interaction()
    {
        interactionInputData.InteractedClicked = Input.GetButtonDown("Interact");
        interactionInputData.Interactedreleased = Input.GetButtonUp("Interact");
    }

    public void curserLock()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void curserUnlock()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public IEnumerator playerDamage()
    {
        playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerDamageFlash.SetActive(false);

    }

    public void enemyHostageCountWait()
    {
        
        

        if (hostageAmount != 0)
        {
            hostageCountText.text = hostageAmount.ToString();
            hostageCurrentText.text = hostageCurrent.ToString();
            CheckNoHostage();
        }
        else
        {
            hostageCountText.text = tempHostage.ToString();
        }
    }

    public void CheckNoHostage()
    {
        if (hostageCurrent == hostageAmount)
        {
            GameManager.instance.youWinText.SetActive(true);
            isMenuOpen = true;
            Menu.SetActive(true);
            curserLock();
        }
    }

    public void updateText()
    {
        enemyHostageCountWait();
    }

    public void checkEnemyTotal()
    {
        enemyAmount--;
        enemyCountText.text = enemyAmount.ToString("F0");

        if (enemyAmount <= 0)
        {
            GameManager.instance.youWinText.SetActive(true);
            isMenuOpen = true;
            Menu.SetActive(true);
            curserLock();
        }
    }
}
