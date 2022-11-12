using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInertatable
{
    public float holdDuration;
    public bool holdInterat;
    public bool multipleUse;
    public bool isIneractable;
    public string ToolTipMessage = "Interact";

    public float HoldDuration => holdDuration;

    public bool HoldInteract => holdInterat;

    public bool MutipleUse => multipleUse;

    public bool IsIteractalbe => isIneractable;

    public string ToolTip => ToolTipMessage;

    public abstract void OnInteract();
}
