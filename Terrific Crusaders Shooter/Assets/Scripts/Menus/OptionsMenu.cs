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

    //void Start()
    //{
    //    if (!PlayerPrefs.HasKey("SFXvolume"))
    //    {
    //        PlayerPrefs.SetFloat("SFXvolume", 1);
    //        LoadSFX();
    //    }
    //    else
    //    {
    //        LoadSFX();
    //    }

    //    if (!PlayerPrefs.HasKey("MusicVolume"))
    //    {
    //        PlayerPrefs.SetFloat("MusicVolume", 1);
    //        LoadMusic();
    //    }
    //    else
    //    {
    //        LoadMusic();
    //    }
    //}
    public void SetSFXVolume()
    {
        SFX.SetFloat("sfxVolume", Mathf.Log10(sfxSlider.value) * 20);
        SaveSFX();
    }

    public void SetMusicVolume()
    {
        music.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20);
        SaveMusic();
    }

    public void Invert(bool isInverted)
    {
        GameManager.instance.cameraControlls.invert = isInverted;
    }

    public void SaveSFX()
    {
        PlayerPrefs.SetFloat("SFXvolume", sfxSlider.value);
    }

    public void LoadSFX()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXvolume");
    }

    public void SaveMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

}
