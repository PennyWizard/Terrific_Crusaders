using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer SFX, music;
    public Slider sfxSlider, musicSlider;

    //public TMP_Text sfxLabel, musicLabel;

    public void SetSFXVolume()
    {
        SFX.SetFloat("sfxVolume", sfxSlider.value);
    }

    public void SetMusicVolume()
    {
        music.SetFloat("musicVolume", musicSlider.value);
    }

    public void Invert(bool isInverted)
    {
        GameManager.instance.cameraControlls.invert = isInverted;
    }

}
