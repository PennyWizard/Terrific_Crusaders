using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInertatable
{
    public float holdDuration;
    public bool holdInterat;
    public bool multipleUse;
    public bool isIneractable;

    public float HoldDuration => holdDuration;

    public bool HoldInteract => holdInterat;

    public bool MutipleUse => multipleUse;

    public bool IsIteractalbe => isIneractable;

    public void OnInteract()
    {
        Debug.Log("Interacted: " + gameObject.name);
    }
}
