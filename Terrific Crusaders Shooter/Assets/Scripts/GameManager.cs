using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int enemyAmount;

    [Header("---Player Stuff---")]
    public GameObject player;
    public PlayerController playerScript;

    public GameObject spawnPosition;

    [Header("---UI---")]
    public GameObject Menu;
    public GameObject playerDamageFlash;
    public Image playerHPBar;
    public Text enemyCountText;
    public GameObject resumButton;
    public GameObject respawnButton;
    public GameObject youWinText;
    public GameObject Introduction;

    public bool isPaused;
    public bool isMenuOpen;

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
        StartCoroutine(enemyCountWait()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isMenuOpen)
        {
            isPaused = !isPaused;

            Menu.SetActive(isPaused);

            if (isPaused)
            {
                curserLock();
                resumButton.SetActive(true);
            }
            else
            {
                curserUnlock();
                resumButton.SetActive(false);
            }
        }
        if (Input.GetButtonDown("Submit"))
        {
            Introduction.SetActive(false);
        }

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

    public IEnumerator enemyCountWait()
    {
        yield return new WaitForSeconds(0.1f);
        enemyCountText.text = enemyAmount.ToString();
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
