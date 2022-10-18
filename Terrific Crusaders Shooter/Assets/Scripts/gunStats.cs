using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    public float shootRate;
    public int shootDist;
    public int shootDmg;
    public int ammoMax;
    public int currentAmmo;
    public GameObject gunModel;
    public AudioClip sound;
    public AudioClip empty;
    public GameObject hitEffect;
    public GameObject muzzleEffect;

}
