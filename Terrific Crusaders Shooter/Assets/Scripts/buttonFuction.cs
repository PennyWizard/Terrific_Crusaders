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
        GameManager.instance.isMenuOpen = false;
        GameManager.instance.gun.isShoot = false;
        GameManager.instance.respawnButton.SetActive(false);
    }

    public void restart()
    {
        GameManager.instance.curserUnlock();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.isMenuOpen = false;
        GameManager.instance.youWinText.SetActive(false);
        GameManager.instance.resumButton.SetActive(false);
        GameManager.instance.respawnButton.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void respawn()
    {
        GameManager.instance.playerScript.Respawm();
        GameManager.instance.curserUnlock();
        GameManager.instance.Menu.SetActive(false);
        GameManager.instance.isMenuOpen = false;
        GameManager.instance.resumButton.SetActive(false);
        GameManager.instance.respawnButton.SetActive(false);
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt("Level", (scene.buildIndex));
    }

}
