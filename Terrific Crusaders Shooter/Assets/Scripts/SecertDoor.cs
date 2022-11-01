using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecertDoor : InteractableBase
{
    [SerializeField] bool isLocked;

    public override void OnInteract()
    {
        if (isLocked)
        {
            if (GameManager.instance.playerScript.hasKey2 == true)
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
