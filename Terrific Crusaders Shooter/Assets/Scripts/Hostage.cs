using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : InteractableBase
{
    private void Start()
    {
        GameManager.instance.hostageAmount++;
    }
    public override void OnInteract()
    {
        GameManager.instance.hostageCurrent++;
        Destroy(gameObject);
        GameManager.instance.updateText();
    }
}
