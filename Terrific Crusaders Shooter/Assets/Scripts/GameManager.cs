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

    //public GameObject spawnPosition;

    //    [Header("---UI---")]
    //    public GameObject pauseMenu;
    //    public GameObject playerDeadMenu;
    //    public GameObject winMenu;
    //    public GameObject menuCurrentlyOpen;
    //    public GameObject playerDamageFlash;
    //    public Image playerHPBar;
    //    public TextMeshProUGUI enemyCountTexy;


    //    public bool isPaused;


    //    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        //spawnPosition = GameObject.FindGameObjectWithTag("Spawn Position");

    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        if (Input.GetButtonDown("Cancel") && !playerDeadMenu.activeSelf)
    //        {
    //            isPaused = !isPaused;
    //            pauseMenu.SetActive(isPaused);

    //            if (isPaused)
    //            {
    //                curserLockPause();
    //            }
    //            else
    //            {
    //                curserUnlockUnpause();
    //            }
    //        }
    //    }

    //    public void curserLockPause()
    //    {
    //        Time.timeScale = 0;
    //        Cursor.visible = true;
    //        Cursor.lockState = CursorLockMode.Confined;
    //    }

    //    public void curserUnlockUnpause()
    //    {
    //        Time.timeScale = 1;
    //        Cursor.visible = false;
    //        Cursor.lockState = CursorLockMode.Locked;
    //    }

    //    public IEnumerator playerDamage()
    //    {
    //        playerDamageFlash.SetActive(true);
    //        yield return new WaitForSeconds(0.1f);
    //        playerDamageFlash.SetActive(false);

    //    }

    public void checkEnemyTotal()
    {
        enemyAmount--;
        //enemyCountTexy.text = enemyAmount.ToString("F0");

        //if (enemyAmount <= 0)
        //{
        //    winMenu.SetActive(true);
        //    curserLockPause();
        //}
    }
}
