using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableBase
{
    [SerializeField] bool isLocked;

    public override void OnInteract()
    {
        if (isLocked)
        {
            if (GameManager.instance.playerScript.hasKey == true)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
