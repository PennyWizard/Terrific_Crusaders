using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QiutGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        int level = PlayerPrefs.GetInt("Level");

        if (level != 0)
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
