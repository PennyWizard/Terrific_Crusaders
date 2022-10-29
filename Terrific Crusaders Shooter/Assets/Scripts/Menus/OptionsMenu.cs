using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer SFX;

    public void SetVolume(float volume)
    {
        SFX.SetFloat("sfxVolume", volume);
    }

    public void Invert(bool isInverted)
    {
        GameManager.instance.cameraControlls.invert = isInverted;
    }

}
