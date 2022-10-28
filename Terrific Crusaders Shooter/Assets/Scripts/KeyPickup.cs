using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : InteractableBase
{
    public override void OnInteract()
    {
        GameManager.instance.playerScript.hasKey = true;
        Destroy(gameObject);
    }
}
