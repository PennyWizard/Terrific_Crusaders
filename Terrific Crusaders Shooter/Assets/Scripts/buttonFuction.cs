using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFuction : MonoBehaviour
{
    public void resume()
    {
        GameManager.instance.curserUnlock();
        GameManager.instance.Menu.SetActive(false);
        GameManager.instance.isPaused = false;
    }

    public void restart()
    {
        GameManager.instance.curserUnlock();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void respawn()
    {
        //GameManager.instance.playerScript.respawn();
        GameManager.instance.curserUnlock();
    }
}
