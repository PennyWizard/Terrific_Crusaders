using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : InteractableBase
{
    public override void OnInteract()
    {
        Destroy(gameObject);
        GameManager.instance.playerScript.hasKey = true;
    }
}
