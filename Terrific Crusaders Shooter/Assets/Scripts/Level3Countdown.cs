using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level3Countdown : MonoBehaviour
{
    private float currentTime;
    public float startTime;
    public Text countDown;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        countDownTimer(manager);
    }

    public void countDownTimer(GameManager manager)
    {
        if (!manager.isPaused)
        {
            currentTime -= 1 * Time.deltaTime;
            countDown.text = currentTime.ToString("0");
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameManager.instance.Menu.SetActive(true);
            GameManager.instance.respawnButton.SetActive(true);
            GameManager.instance.curserLock();
            GameManager.instance.isMenuOpen = true;
        }
    }

}
