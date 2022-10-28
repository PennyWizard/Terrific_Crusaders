using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.playerScript.hasKey = true;
        Destroy(gameObject);
    }
}
