using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInertatable 
{
    
    float HoldDuration { get; }

    bool HoldInteract { get; }

    bool MutipleUse { get; }

    bool IsIteractalbe { get; }

    void OnInteract();

}
