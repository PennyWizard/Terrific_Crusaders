using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TastSoundMaker : MonoBehaviour
{
    [SerializeField] AudioSource source = null;

    [SerializeField] float soundRange = 25f;

    private void OnMouseDown()
    {
        if (source.isPlaying)
        {
            return;
        }

        source.Play();

        var sound = new Sound(transform.position, soundRange);

        sound.soundType = Sound.SoundType.Danger;

        Sounds.MakeSound(sound);
    }
}
