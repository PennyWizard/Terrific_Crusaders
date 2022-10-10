using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFuction : MonoBehaviour
{
    public void resume()
    {
        //gameManager.instance.cursorUnLockPause();
        //gameManager.instance.pauseMenu.SetActive(false);
        //gameManager.instance.isPaused = false;
    }

    public void restart()
    {
        //gameManager.instance.cursorUnLockPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void respawn()
    {
        //gameManager.instance.playerScript.respawn();
        //gameManager.instance.cursorUnLockPause();
    }
}
