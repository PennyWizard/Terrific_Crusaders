using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoading : MonoBehaviour
{
    public OptionsMenu optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SFXvolume"))
        {
            PlayerPrefs.SetFloat("SFXvolume", 1);
            optionsMenu.LoadSFX();
        }
        else
        {
            optionsMenu.LoadSFX();
        }

        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            optionsMenu.LoadMusic();
        }
        else
        {
            optionsMenu.LoadMusic();
        }
    }

    
}
