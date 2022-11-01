using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTwoPickup : InteractableBase
{
    public override void OnInteract()
    {
        GameManager.instance.playerScript.hasKey2 = true;
        Destroy(gameObject);
    }
}
