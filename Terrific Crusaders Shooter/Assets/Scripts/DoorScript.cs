using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableBase
{
    

    public override void OnInteract()
    {
        if (GameManager.instance.playerScript.hasKey == true)
        {
            Destroy(gameObject);
        }
    }
}
